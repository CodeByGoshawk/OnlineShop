using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
using PublicTools.Resources;
using ResponseFramewrok;
using System.Collections.Generic;

namespace OnlineShop.RepositoryDesignPattern.Services.SaleRepositories;
public class OrderRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, OrderHeader, Guid>(dbContext), IOrderRepository
{
    public async override Task<IResponse<List<OrderHeader>>> SelectAllAsync()
    {
        var entity = await _dbSet
            .Include(e => e.Buyer)
            .AsNoTracking()
            .ToListAsync();
        return entity is not null ? new Response<List<OrderHeader>>(entity) : new Response<List<OrderHeader>>(MessageResource.Error_FindEntityFailed);
    }

    public async Task<IResponse<IEnumerable<OrderHeader>>> SelectRangeBySellerAsync(string sellerId)
    {
        if (sellerId is null) return new Response<IEnumerable<OrderHeader>>(MessageResource.Error_NullInputCode);
        var sellerOrders = await _dbSet
            .Where(o => !o.IsSoftDeleted)
            .Where(o => o.OrderDetails.Select(od => od.Product).Select(p => p.SellerId).Contains(sellerId))
            .Include(o => o.Buyer)
            .AsNoTracking()
            .ToListAsync();
        return new Response<IEnumerable<OrderHeader>>(sellerOrders);
    }

    public async Task<IResponse<IEnumerable<OrderHeader>>> SelectRangeByBuyerAsync(string buyerId)
    {
        if (buyerId is null) return new Response<IEnumerable<OrderHeader>>(MessageResource.Error_NullInputCode);
        var buyerOrders = await _dbSet
            .Where(o => o.BuyerId == buyerId && !o.IsSoftDeleted)
            .AsNoTracking()
            .ToListAsync();
        return new Response<IEnumerable<OrderHeader>>(buyerOrders);
    }

    public async override Task<IResponse<OrderHeader>> SelectByIdAsync(Guid id)
    {
        var order = await _dbSet
            .Include(e => e.OrderDetails).ThenInclude(e => e.Product!.SellerId)
            .Include(e => e.Buyer)
            .AsNoTracking()
            .SingleOrDefaultAsync(oh => oh.Id == id);
        return order is not null ? new Response<OrderHeader>(order) : new Response<OrderHeader>(MessageResource.Error_FindEntityFailed);
    }

    public async Task<IResponse<OrderHeader>> SelectByCodeAsync(string code)
    {
        if (code is null) return new Response<OrderHeader>(MessageResource.Error_NullInputCode);
        var order = await _dbSet
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Code == code);
        return order is null ? new Response<OrderHeader>(MessageResource.Error_FindEntityFailed) : new Response<OrderHeader>(order);
    }

    public async Task<IResponse<object>> ClearOrderDetails(OrderHeader entity)
    {
        _dbContext.ChangeTracker.Entries<OnlineShopUser>().ToList().ForEach(u => u.State = EntityState.Detached);

        _dbContext.RemoveRange(entity.OrderDetails);

        return new Response<object>(entity);
    }
}
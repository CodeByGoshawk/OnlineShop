using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Services.Sale;
public class OrderRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, OrderHeader, Guid>(dbContext), IOrderRepository
{
    public async override Task<IResponse<OrderHeader>> SelectByIdAsync(Guid id)
    {
        var entity = await _dbSet
            .Include(e => e.OrderDetails)
            .Include(e => e.Seller)
            .Include(e => e.Buyer)
            .AsNoTracking()
            .SingleOrDefaultAsync(oh => oh.Id == id);
        return entity is not null ? new Response<OrderHeader>(entity) : new Response<OrderHeader>(MessageResource.Error_FindEntityFailed);
    }

    public async Task<IResponse<OrderHeader>> SelectByCodeAsync(string code)
    {
        if (code is null) return new Response<OrderHeader>(MessageResource.Error_NullInputCode);
        var orderHeader = await _dbSet
            .Include(e => e.OrderDetails)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Code == code);
        return orderHeader is null ? new Response<OrderHeader>(MessageResource.Error_FindEntityFailed) : new Response<OrderHeader>(orderHeader);
    }

    public async Task<IResponse<object>> ClearOrderDetails(OrderHeader entity)
    {
        var trackedUsers = _dbContext.ChangeTracker.Entries<OnlineShopUser>();

        foreach (var trackedUser in trackedUsers)
        {
            trackedUser.State = EntityState.Detached;
        }

        _dbContext.RemoveRange(entity.OrderDetails);

        return new Response<object>(entity);
    }
}
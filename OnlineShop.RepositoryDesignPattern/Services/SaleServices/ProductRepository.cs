using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Services.SaleRepositories;
public class ProductRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, Product, Guid>(dbContext), IProductRepository
{
    public async Task<IResponse<Product>> SelectNonDeletedByIdAsync(Guid id)
    {
        var product = await _dbSet.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id && !p.IsSoftDeleted);
        return product is not null ? new Response<Product>(product) : new Response<Product>(MessageResource.Error_FindEntityFailed);
    }

    public async Task<IResponse<Product>> SelectByCodeAsync(string code)
    {
        if (code is null) return new Response<Product>(MessageResource.Error_NullInputCode);
        var product = await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Code == code);
        return product is null ? new Response<Product>(MessageResource.Error_FindEntityFailed) : new Response<Product>(product);
    }

    public async Task<IResponse<IEnumerable<Product>>> SelectAllNonDeletedsAsync()
    {
        var nodeDeletedProducts = await _dbSet
            .Where(o => !o.IsSoftDeleted)
            .AsNoTracking()
            .ToListAsync();
        return new Response<IEnumerable<Product>>(nodeDeletedProducts);
    }

    public async Task<IResponse<IEnumerable<Product>>> SelectNonDeletedsBySellerAsync(string sellerId)
    {
        if (sellerId is null) return new Response<IEnumerable<Product>>(MessageResource.Error_NullInputCode);
        var sellerProducts = await _dbSet
            .Where(o => o.SellerId == sellerId && !o.IsSoftDeleted)
            .AsNoTracking()
            .ToListAsync();
        return new Response<IEnumerable<Product>>(sellerProducts);
    }
}
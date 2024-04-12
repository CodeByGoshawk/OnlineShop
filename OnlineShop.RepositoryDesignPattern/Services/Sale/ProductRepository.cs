using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Services.Sale;
public class ProductRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, Product, Guid>(dbContext) , IProductRepository
{
    public async Task<IResponse<Product>> SelectByCodeAsync(string code)
    {
        if (code is null) return new Response<Product>(MessageResource.Error_NullInputCode);
        var product = await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Code == code);
        return product is null ? new Response<Product>(MessageResource.Error_FindEntityFailed) : new Response<Product>(product);
    }
}
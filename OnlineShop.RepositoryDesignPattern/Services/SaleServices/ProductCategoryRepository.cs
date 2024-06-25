using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Services.SaleRepositories;
public class ProductCategoryRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, ProductCategory, int>(dbContext), IProductCategoryRepository
{
    public async override Task<IResponse<ProductCategory>> SelectByIdAsync(int id)
    {
        var entity = await _dbSet
            .Include(e => e.Products)
            .AsNoTracking()
            .SingleOrDefaultAsync(pc => pc.Id == id);
        return entity is not null ? new Response<ProductCategory>(entity) : new Response<ProductCategory>(MessageResource.Error_FindEntityFailed);
    }
}
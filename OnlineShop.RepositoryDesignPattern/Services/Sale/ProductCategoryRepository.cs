using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;

namespace OnlineShop.RepositoryDesignPattern.Services.Sale;
public class ProductCategoryRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, ProductCategory, int>(dbContext), IProductCategoryRepository
{
}
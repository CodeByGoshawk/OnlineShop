using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Frameworks.Bases;

namespace OnlineShop.RepositoryDesignPattern.Services.Sale;
public class ProductRepository(OnlineShopDbContext dbContext) : BaseRepository<OnlineShopDbContext, Product, Guid>(dbContext) , IProductRepository
{
}
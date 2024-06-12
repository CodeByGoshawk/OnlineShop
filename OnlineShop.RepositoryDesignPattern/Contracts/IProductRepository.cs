using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IProductRepository : IRepository<Product, Guid>
{
    Task<IResponse<Product>> SelectByCodeAsync(string code);
    Task<IResponse<IEnumerable<Product>>> SelectBySellerAsync(string sellerId);
}

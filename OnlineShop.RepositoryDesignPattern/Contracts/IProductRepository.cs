using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IProductRepository : IRepository<Product,Guid>
{
}

using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IProductCategoryRepository : IRepository<ProductCategory, int>
{
}

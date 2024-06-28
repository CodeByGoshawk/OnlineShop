using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IProductCategoryRepository : IRepository<ProductCategory, int>
{
    IResponse<IQueryable<ProductCategory>> SelectAll();
}

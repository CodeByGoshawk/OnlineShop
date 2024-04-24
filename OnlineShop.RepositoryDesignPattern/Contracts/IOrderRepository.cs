using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IOrderRepository : IRepository<OrderHeader, Guid>
{
    Task<IResponse<OrderHeader>> SelectByCodeAsync(string code);

    Task<IResponse<object>> ClearOrderDetails(OrderHeader entity);
}

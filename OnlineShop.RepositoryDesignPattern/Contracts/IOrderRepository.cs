using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IOrderRepository : IRepository<OrderHeader, Guid>
{
    Task<IResponse<OrderHeader>> SelectNonDeletedByIdAsync(Guid id);
    Task<IResponse<OrderHeader>> SelectByCodeAsync(string code);

    Task<IResponse<IEnumerable<OrderHeader>>> SelectNonDeletedsBySellerAsync(string sellerId);
    Task<IResponse<IEnumerable<OrderHeader>>> SelectNonDeletedsByBuyerAsync(string buyerId);
}

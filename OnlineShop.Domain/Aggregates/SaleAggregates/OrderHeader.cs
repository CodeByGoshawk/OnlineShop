using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Domain.Frameworks.Abstracts;
using OnlineShop.Domain.Frameworks.Bases;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class OrderHeader : MainEntityBase, ICreatedEntity
{
    public string BuyerId { get; set; }

    public OnlineShopUser? Buyer { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = [];
}

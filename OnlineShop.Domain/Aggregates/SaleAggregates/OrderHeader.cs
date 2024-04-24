using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Domain.Frameworks.Abstracts;
using OnlineShop.Domain.Frameworks.Bases;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class OrderHeader : MainEntityBase, ICreatedEntity
{
    // Keys
    public string SellerId { get; set; }
    public string BuyerId { get; set; }

    // Navigations
    public OnlineShopUser? Seller { get; set; }
    public OnlineShopUser? Buyer { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = [];
}

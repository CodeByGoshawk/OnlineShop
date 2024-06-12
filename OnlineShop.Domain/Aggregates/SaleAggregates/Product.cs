using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Domain.Frameworks.Abstracts;
using OnlineShop.Domain.Frameworks.Bases;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class Product : MainEntityBase, ITitledEntity
{
    // Keys
    public int ProductCategoryId { get; set; }
    public string? SellerId { get; set; }

    // Properties
    public string? Title { get; set; }
    public decimal UnitPrice { get; set; }

    // Navigations
    public ProductCategory? ProductCategory { get; set; }
    public OnlineShopUser? Seller { get; set; }
}
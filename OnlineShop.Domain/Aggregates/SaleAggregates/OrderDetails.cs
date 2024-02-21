using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class OrderDetails : IDbSetEntity
{
    //Keys
    public OrderHeader OrderHeader { get; set; }
    public Product Product { get; set; }

    // Properties
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
}

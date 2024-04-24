using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class OrderDetail : IDbSetEntity
{
    //Keys
    public Guid OrderHeaderId { get; set; }
    public Guid ProductId { get; set; }


    // Properties
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }

    // Navigations
    public OrderHeader? OrderHeader { get; set; }
    public Product? Product { get; set; }
}

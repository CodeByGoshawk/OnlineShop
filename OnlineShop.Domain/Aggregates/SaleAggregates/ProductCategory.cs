using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class ProductCategory : IEntity<int>, IDbSetEntity, ITitledEntity
{
    // Keys
    public int Id { get; set; }
    public ProductCategory Parent { get; set; }

    // Properties
    public string Title { get; set; }
}
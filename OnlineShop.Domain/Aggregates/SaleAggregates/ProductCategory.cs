using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class ProductCategory : IEntity<int>, IDbSetEntity, ITitledEntity
{
    // Keys
    public int Id { get; set; }
    public int? ParentId { get; set; }

    // Properties
    public string Title { get; set; }

    // Navigations
    public ProductCategory? Parent { get; set; }
}
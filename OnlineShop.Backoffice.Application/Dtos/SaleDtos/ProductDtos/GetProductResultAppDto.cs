using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductResultAppDto
{
    public Guid Id { get; set; }
    public int ProductCategoryId { get; set; }
    [OwnerId]
    public string? SellerId { get; set; }

    public decimal UnitPrice { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }

    public bool IsSoftDeleted { get; set; }
    public DateTime? SoftDeleteDateGregorian { get; set; }
    public string? SoftDeleteDatePersian { get; set; }

    public ProductCategory? ProductCategory { get; set; }
}

using OnlineShop.Domain.Aggregates.SaleAggregates;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductResultAppDto
{
    public Guid Id { get; set; }
    public int ProductCategoryId { get; set; }

    public decimal UnitPrice { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string? Picture { get; set; }
}

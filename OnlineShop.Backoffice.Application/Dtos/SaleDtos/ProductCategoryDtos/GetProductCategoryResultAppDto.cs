using OnlineShop.Domain.Aggregates.SaleAggregates;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class GetProductCategoryResultAppDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    public string Title { get; set; }

    public GetProductCategoryResultAppDto? Parent { get; set; }
}
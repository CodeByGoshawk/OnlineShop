using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class GetProductCategoryResultAppDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    public string Title { get; set; }

    public GetProductCategoryResultAppDto? Parent { get; set; }
    public List<GetProductResultAppDto>? Products { get; set; }
}
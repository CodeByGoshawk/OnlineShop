using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
using OnlineShop.Domain.Aggregates.SaleAggregates;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class GetProductCategoryResultAppDto : IIdentifiedDto<int>, ITitledDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    public string Title { get; set; }

    public ProductCategory? Parent { get; set; }
    public List<Product>? Products { get; set; }
}
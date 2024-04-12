using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class PutProductCategoryAppDto :IIdentifiedDto<int> , ITitledDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    public string Title { get; set; }
}

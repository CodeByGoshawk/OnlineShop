using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class PostProductCategoryAppDto : ITitledDto
{
    public int? ParentId { get; set; }

    public string Title { get; set; }
}

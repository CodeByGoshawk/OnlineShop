using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
public class PostProductAppDto : ICodedDto<string>, ITitledDto
{
    public int ProductCategoryId { get; set; }

    public string Code { get; set; }
    public string Title { get; set; }

    public decimal UnitPrice { get; set; }
}

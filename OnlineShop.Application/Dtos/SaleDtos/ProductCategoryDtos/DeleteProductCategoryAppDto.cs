using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class DeleteProductCategoryAppDto : IIdentifiedDto<int>
{
    public int Id { get; set; }
}

using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class GetProductCategoryAppDto : IIdentifiedDto<int>
{
    public int Id { get; set; }
}

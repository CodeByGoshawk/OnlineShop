using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
public class DeleteProductAppDto : IIdentifiedDto<Guid>
{
    public Guid Id { get; set; }
}

using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.OrderDtos;
public class DeleteOrderAppDto : IIdentifiedDto<Guid>
{
    public Guid Id { get; set; }
}

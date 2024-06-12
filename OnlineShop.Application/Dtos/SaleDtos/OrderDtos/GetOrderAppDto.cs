using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderAppDto : IIdentifiedDto<Guid>
{
    public Guid Id { get; set; }
}

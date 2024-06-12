using OnlineShop.Application.Dtos.SaleDtos.OrderDtos;
using OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;

namespace OnlineShop.Application.Contracts.Sale;
public interface IOrderService : IService<GetOrderAppDto, GetOrderResultAppDto, GetAllOrdersResultAppDto, PostOrderAppDto, PutOrderAppDto, DeleteOrderAppDto, Guid>
{
}

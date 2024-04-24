using OnlineShop.Application.Dtos.SaleDtos.OrderHeaderDtos;
using OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;

namespace OnlineShop.Application.Contracts;
public interface IOrderService : IService<GetOrderAppDto,GetOrderResultAppDto,GetAllOrdersResultAppDto,PostOrderAppDto,PutOrderAppDto,DeleteOrderAppDto,Guid>
{
}

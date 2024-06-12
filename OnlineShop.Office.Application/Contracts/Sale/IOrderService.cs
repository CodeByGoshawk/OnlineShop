using OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.Sale;
public interface IOrderService
{
    Task<IResponse<GetOrderResultAppDto>> Get(GetOrderAppDto model);
    Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeByBuyer(GetOrdersRangeByBuyerAppDto model);
    Task<IResponse<object>> Post(PostOrderAppDto model);
    Task<IResponse<object>> Put(PutOrderAppDto model);
    Task<IResponse<object>> Delete(DeleteOrderAppDto model);
}

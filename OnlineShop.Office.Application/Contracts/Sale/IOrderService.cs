using OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.Sale;
public interface IOrderService
{
    Task<IResponse<GetOrderResultAppDto>> Get(GetOrderAppDto model);
    Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeByBuyer(GetOrdersRangeByBuyerAppDto model);
    Task<IResponse> Post(PostOrderAppDto model);
    Task<IResponse> Put(PutOrderAppDto model);
    Task<IResponse> Delete(DeleteOrderAppDto model);
}

using OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.Sale;
public interface IOrderService
{
    Task<IResponse<GetOrdersRangeResultAppDto>> GetAllWithPrivateData();
    Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeBySeller(GetOrdersRangeBySellerAppDto model);
    Task<IResponse<GetOrderResultAppDto>> GetWithPrivateData(GetOrderAppDto model);
    Task<IResponse<GetOrderResultAppDto>> GetWithSellerOrderDetails(GetOrderAppDto model);
    Task<IResponse<object>> Put(PutOrderAppDto model);
    Task<IResponse<object>> Delete(DeleteOrderAppDto model);
}

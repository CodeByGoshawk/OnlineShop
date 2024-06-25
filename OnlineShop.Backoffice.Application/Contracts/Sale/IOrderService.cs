using OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.Sale;
public interface IOrderService
{
    Task<IResponse<GetOrdersRangeWithPrivateDataResultAppDto>> GetAllWithPrivateData();
    Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeBySeller(GetOrdersRangeBySellerAppDto model);
    Task<IResponse<GetOrderWithPrivateDataResultAppDto>> GetWithPrivateData(GetOrderAppDto model);
    Task<IResponse<GetOrderResultAppDto>> GetWithSellerOrderDetails(GetOrderAppDto model);
    Task<IResponse> Put(PutOrderAppDto model);
    Task<IResponse> Delete(DeleteOrderAppDto model);
}

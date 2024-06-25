using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.UserManagement;
public interface IUserService
{
    Task<IResponse<GetOnlineShopUserResultAppDto>> Get(GetOnlineShopUserAppDto model);
    Task<IResponse> Post(PostOnlineShopUserAppDto model);
    Task<IResponse> Put(PutOnlineShopUserAppDto model);
    Task<IResponse> ChangePassword(ChangePasswordAppDto model);
    Task<IResponse> Delete(DeleteOnlineShopUserAppDto model);
}

using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.UserManagement;
public interface IUserService
{
    Task<IResponse<GetOnlineShopUserResultAppDto>> Get(GetOnlineShopUserAppDto model);
    Task<IResponse<object>> Post(PostOnlineShopUserAppDto model);
    Task<IResponse<object>> Put(PutOnlineShopUserAppDto model);
    Task<IResponse<object>> ChangePassword(ChangePasswordAppDto model);
    Task<IResponse<object>> Delete(DeleteOnlineShopUserAppDto model);
}

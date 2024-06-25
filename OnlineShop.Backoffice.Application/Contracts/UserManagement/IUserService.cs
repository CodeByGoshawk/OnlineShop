using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.UserManagement;
public interface IUserService
{
    Task<IResponse<GetAllUsersWithPrivateDataResultAppDto>> GetAllWithPrivateData(GetAllUsersAppDto model);

    Task<IResponse<GetUserResultAppDto>> Get(GetUserAppDto model);
    Task<IResponse<GetUserWithPrivateDataResultAppDto>> GetWithPrivateData(GetUserAppDto model);

    Task<IResponse> Register(RegisterUserAppDto model);
    Task<IResponse> Edit(EditUserAppDto model);
    Task<IResponse> ChangePassword(ChangePasswordAppDto model);
    Task<IResponse> GrantOrRevokeAdminRole(GrantOrRevokeRoleAppDto model);
    Task<IResponse> GrantOrRevokeSellerRole(GrantOrRevokeRoleAppDto model);

    Task<IResponse> Delete(DeleteUserAppDto model);

    //Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetSellers();
}

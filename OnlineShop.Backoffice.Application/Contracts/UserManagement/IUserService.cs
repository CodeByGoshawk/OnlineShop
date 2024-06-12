using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.UserManagement;
public interface IUserService
{
    Task<IResponse<GetOnlineShopUserResultAppDto>> Get(GetOnlineShopUserAppDto model);
    Task<IResponse<GetOnlineShopUserResultAppDto>> GetNonAdminWithPrivateData(GetOnlineShopUserAppDto model);
    Task<IResponse<GetOnlineShopUserResultAppDto>> GetWithPrivateData(GetOnlineShopUserAppDto model);
    Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetNonAdminsWithPrivateData();
    Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetAllWithPrivateData();
    
    //Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetSellers();

    Task<IResponse<object>> Register(RegisterOnlineShopUserAppDto model);
    Task<IResponse<object>> EditSelf(EditOnlineShopUserPropertiesAppDto model);
    Task<IResponse<object>> EditNonGodAdmin(EditOnlineShopUserPropertiesAppDto model);
    Task<IResponse<object>> EditNonAdmin(EditOnlineShopUserPropertiesAppDto model);
    Task<IResponse<object>> ChangePassword(ChangePasswordAppDto model);
    Task<IResponse<object>> DeleteNonGodAdmin(DeleteOnlineShopUserAppDto model);
    Task<IResponse<object>> DeleteNonAdmin(DeleteOnlineShopUserAppDto model);
    Task<IResponse<object>> GrantOrRevokeAdminRole(GrantOrRevokeAdminRoleAppDto model);
}

using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.RoleDtos;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.UserManagement;
public interface IRoleService
{
    Task<IResponse<GetOnlineShopRoleResultAppDto>> Get(GetOnlineShopRoleAppDto model);
    Task<IResponse<GetAllOnlineShopRolesResultAppDto>> GetAll();
    Task<IResponse> Register(RegisterOnlineShopRoleAppDto model);
    Task<IResponse> Edit(EditOnlineShopRoleAppDto model);
    Task<IResponse> Delete(DeleteUserAppDto model);
}

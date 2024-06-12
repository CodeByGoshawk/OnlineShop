using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.AccountDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.UserManagement;
public interface IAccountService
{
    Task<IResponse<LoginResultAppDto>> Login(LoginAppDto model);
}

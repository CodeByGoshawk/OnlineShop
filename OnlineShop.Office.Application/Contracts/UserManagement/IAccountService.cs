using OnlineShop.Office.Application.Dtos.UserManagementDtos.AccountDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.UserManagement;
public interface IAccountService
{
    Task<IResponse<LoginResultAppDto>> Login(LoginAppDto model);
}

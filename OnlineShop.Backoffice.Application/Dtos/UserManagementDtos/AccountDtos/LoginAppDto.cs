using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.AccountDtos;
public class LoginAppDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}

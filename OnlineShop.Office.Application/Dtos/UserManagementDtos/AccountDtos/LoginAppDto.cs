using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.AccountDtos;
public class LoginAppDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}

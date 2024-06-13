using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class ChangePasswordAppDto
{
    [OwnerId]
    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }
    [Required(ErrorMessage = "NewPassword is required")]
    public string NewPassword { get; set; }
    [Required(ErrorMessage = "CurrentPassword is required")]
    public string CurrentPassword { get; set; }
}

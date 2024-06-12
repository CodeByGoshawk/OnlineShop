using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class EditOnlineShopUserRolesAppDto
{
    [Required(ErrorMessage = "Id Is Required")]
    public string Id { get; set; }

    [Required(ErrorMessage = "Roles Is Required")]
    public List<string> Roles { get; set; }
}

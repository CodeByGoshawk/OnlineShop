using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class EditUserRolesAppDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public List<string> Roles { get; set; }
}

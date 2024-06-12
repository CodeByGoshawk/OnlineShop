using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class PutOnlineShopUserPropertiesAppDto
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "National Id is required")]
    public string NationalId { get; set; }

    [Required(ErrorMessage = "Cell Phone is required")]
    public string CellPhone { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Picture { get; set; }
    public string? Location { get; set; }
}

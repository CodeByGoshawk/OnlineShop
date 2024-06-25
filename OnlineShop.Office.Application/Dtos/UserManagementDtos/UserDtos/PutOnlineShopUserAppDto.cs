using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class PutOnlineShopUserAppDto
{
    [RequesterId]
    public string? Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, RegularExpression("^\\d{10}$", ErrorMessage = "National Id is wrong")]
    public string NationalId { get; set; }

    [Required, RegularExpression("^[0-9]*$", ErrorMessage = "CellPhone is wrong")]
    public string CellPhone { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }



    [RegularExpression("^[0-9]*$", ErrorMessage = "Phone is wrong")]
    public string? PhoneNumber { get; set; }
    public string? Picture { get; set; }
    public string? Location { get; set; }
}

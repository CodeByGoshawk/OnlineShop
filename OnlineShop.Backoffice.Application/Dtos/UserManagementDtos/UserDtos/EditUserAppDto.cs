using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class EditUserAppDto
{
    [Required, JsonPropertyName("Id")]
    public string UserToEditId { get; set; }

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


    [RequesterId]
    public string? EditorUserId { get; set; }
}

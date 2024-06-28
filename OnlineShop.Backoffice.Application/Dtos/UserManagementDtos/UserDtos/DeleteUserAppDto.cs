using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class DeleteUserAppDto
{
    [RequesterId]
    public string? DeleterUserId { get; set; }

    [Required, JsonPropertyName("Id")]
    public string UserToDeleteId { get; set; }
}

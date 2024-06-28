using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetUserAppDto
{
    [RequesterId]
    public string? GetterUserId { get; set; }

    [Required,JsonPropertyName("Id")]
    public string UserToGetId { get; set; }
}

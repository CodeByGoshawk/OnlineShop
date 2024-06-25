using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetUserAppDto
{
    [RequesterId]
    public string? GetterUserId { get; set; }

    [Required]
    public string UserToGetId { get; set; }
}

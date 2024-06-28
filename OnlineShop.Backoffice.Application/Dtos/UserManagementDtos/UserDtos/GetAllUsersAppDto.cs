using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetAllUsersAppDto
{
    [RequesterId]
    public string? GetterUserId { get; set; }
}

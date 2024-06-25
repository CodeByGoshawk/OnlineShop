using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class DeleteUserAppDto
{
    [RequesterId]
    public string? DeleterUserId { get; set; }
    public string UserToDeleteId { get; set; }
}

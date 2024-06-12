using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetOnlineShopUserAppDto
{
    [OwnerId]
    public string Id { get; set; }
}

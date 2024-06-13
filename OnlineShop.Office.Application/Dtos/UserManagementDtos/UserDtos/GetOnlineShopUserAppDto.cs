using PublicTools.Attributes;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class GetOnlineShopUserAppDto
{
    [OwnerId]
    public string Id { get; set; }
}

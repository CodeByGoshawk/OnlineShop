using PublicTools.Attributes;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class GetOnlineShopUserAppDto
{
    [RequesterId]
    public string Id { get; set; }
}

using PublicTools.Attributes;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class DeleteOnlineShopUserAppDto
{
    [RequesterId]
    public string Id { get; set; }
}

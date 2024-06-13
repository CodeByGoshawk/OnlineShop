using PublicTools.Attributes;

namespace OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
public class DeleteOnlineShopUserAppDto
{
    [OwnerId]
    public string Id { get; set; }
}

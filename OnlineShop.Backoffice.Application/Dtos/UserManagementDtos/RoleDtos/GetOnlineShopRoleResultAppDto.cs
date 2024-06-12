namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.RoleDtos;
public class GetOnlineShopRoleResultAppDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? NormalizedName { get; set; }
    public string? ConcurrencyStamp { get; set; }
}

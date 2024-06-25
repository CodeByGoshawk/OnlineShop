using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using PublicTools.Attributes;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderResultAppDto
{
    public Guid Id { get; set; }
    [RequesterId]
    public string BuyerId { get; set; }
    public string Code { get; set; }
    public GetOnlineShopUserResultAppDto? Buyer { get; set; }
    public List<OrderDetailAppDto> OrderDetails { get; set; } = [];

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }
}

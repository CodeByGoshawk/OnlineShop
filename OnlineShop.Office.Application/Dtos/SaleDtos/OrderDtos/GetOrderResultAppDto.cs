using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderResultAppDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public GetOnlineShopUserResultAppDto? Buyer { get; set; }
    public List<OrderDetailAppDto> OrderDetailDtos { get; set; } = [];

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }
}

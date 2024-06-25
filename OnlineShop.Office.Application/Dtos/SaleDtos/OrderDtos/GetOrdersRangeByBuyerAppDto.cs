using PublicTools.Attributes;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrdersRangeByBuyerAppDto
{
    [RequesterId]
    public string BuyerId { get; set; }
}

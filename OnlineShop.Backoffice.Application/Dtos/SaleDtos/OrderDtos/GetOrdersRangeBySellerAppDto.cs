using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrdersRangeBySellerAppDto
{
    [RequesterId]
    public string? SellerId { get; set; }
}

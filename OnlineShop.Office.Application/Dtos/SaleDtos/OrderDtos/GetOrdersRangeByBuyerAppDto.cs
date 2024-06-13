using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrdersRangeByBuyerAppDto
{
    [OwnerId]
    [Required(ErrorMessage = "Buyer Id is required")]
    public string BuyerId { get; set; }
}

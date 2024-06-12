using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrdersRangeBySellerAppDto
{
    [OwnerId]
    [Required(ErrorMessage = "Seller Id is required")]
    public string SellerId { get; set; }
}

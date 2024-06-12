using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderAppDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [OwnerId]
    [Required(ErrorMessage = "Seller Id is required")]
    public string SellerId { get; set; }
}

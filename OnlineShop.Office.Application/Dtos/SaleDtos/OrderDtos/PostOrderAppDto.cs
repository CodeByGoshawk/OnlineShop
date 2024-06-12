using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class PostOrderAppDto
{
    [Required(ErrorMessage = "Buyer Id is required")]
    public string BuyerId { get; set; }

    [Required(ErrorMessage = "OrderDetailDtos are required")]
    public List<OrderDetailAppDto> OrderDetailDtos { get; set; }
}
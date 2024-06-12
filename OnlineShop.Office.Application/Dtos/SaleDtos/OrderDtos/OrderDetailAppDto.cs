using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;

public class OrderDetailAppDto
{
    [Required(ErrorMessage = "OrderHeaderId is required")]
    public Guid? OrderHeaderId { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;

public class OrderDetailAppDto
{
    [Required]
    public Guid? OrderHeaderId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}

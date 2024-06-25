using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;

public class OrderDetailAppDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    public decimal? UnitPrice { get; set; }
    public string? SellerId { get; set; }
}

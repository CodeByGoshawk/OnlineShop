using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderAppDto
{
    [Required]
    public Guid Id { get; set; }

    [RequesterId]
    public string? BuyerId { get; set; }
}

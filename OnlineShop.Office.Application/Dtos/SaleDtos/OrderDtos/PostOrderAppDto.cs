using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class PostOrderAppDto
{
    [RequesterId]
    public string? BuyerId { get; set; }

    [Required]
    public List<OrderDetailAppDto> OrderDetailDtos { get; set; }
}
using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductAppDto
{
    [Required]
    public Guid Id { get; set; }

    [RequesterId]
    public string? SellerId { get; set; }
}

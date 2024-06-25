using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class PutProductAppDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public int ProductCategoryId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    public string? Picture { get; set; }

    [RequesterId]
    public string? SellerId { get; set; }
}

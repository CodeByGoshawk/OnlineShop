using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class DeleteProductAppDto
{
    [Required]
    public Guid Id { get; set; }

    [RequesterId]
    public string? DeleterUserId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductAppDto
{
    [Required]
    public Guid Id { get; set; }
}

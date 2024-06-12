using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductAppDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
}

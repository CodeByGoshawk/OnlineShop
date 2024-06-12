using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductAppDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class DeleteOrderAppDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
}

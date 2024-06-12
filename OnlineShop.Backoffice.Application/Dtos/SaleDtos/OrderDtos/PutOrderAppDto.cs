using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class PutOrderAppDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "OrderDetailDtos are required")]
    public List<OrderDetailAppDto> OrderDetailDtos { get; set; }

}

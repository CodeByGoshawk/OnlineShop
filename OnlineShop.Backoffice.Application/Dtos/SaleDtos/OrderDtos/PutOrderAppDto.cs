using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class PutOrderAppDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public List<OrderDetailAppDto> OrderDetailDtos { get; set; }

}

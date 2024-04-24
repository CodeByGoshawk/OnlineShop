using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;

namespace OnlineShop.Application.Dtos.SaleDtos.OrderHeaderDtos;
public class GetAllOrdersResultAppDto
{
    public List<GetOrderResultAppDto> GetResultDtos { get; set; } = [];
}

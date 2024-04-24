using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.OrderHeaderDtos;
public class PostOrderAppDto : ICodedDto<string>
{
    public string Code { get; set; }
    public string SellerId { get; set; }
    public string BuyerId { get; set; }
    public List<OrderDetailDto> OrderDetailDtos { get; set; }
}
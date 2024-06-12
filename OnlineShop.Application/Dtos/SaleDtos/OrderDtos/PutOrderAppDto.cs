using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;

namespace OnlineShop.Application.Dtos.SaleDtos.OrderDtos;
public class PutOrderAppDto : IIdentifiedDto<Guid>, ICodedDto<string>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string SellerId { get; set; }
    public string BuyerId { get; set; }
    public List<OrderDetailDto> OrderDetailDtos { get; set; }

}

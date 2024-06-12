using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;

namespace OnlineShop.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderResultAppDto : IIdentifiedDto<Guid>, ICodedDto<string>, ICreatedDto, IModifiedDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public OnlineShopUser? Seller { get; set; }
    public OnlineShopUser? Buyer { get; set; }
    public List<OrderDetailDto> OrderDetailDtos { get; set; } = [];

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }
}

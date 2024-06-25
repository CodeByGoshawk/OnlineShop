using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrderWithPrivateDataResultAppDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public GetUserResultAppDto? Buyer { get; set; }
    public List<OrderDetailAppDto> OrderDetails { get; set; } = [];

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }

    public bool IsSoftDeleted { get; set; }
    public DateTime? SoftDeleteDateGregorian { get; set; }
    public string? SoftDeleteDatePersian { get; set; }
}

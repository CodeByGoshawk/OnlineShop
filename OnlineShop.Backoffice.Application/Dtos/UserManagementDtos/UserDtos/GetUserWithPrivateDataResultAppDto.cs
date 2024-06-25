using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetUserWithPrivateDataResultAppDto
{
    [RequesterId]
    public string? Id { get; set; }
    public string? UserName { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? NationalId { get; set; }
    public bool IsNationalIdConfirmed { get; set; }

    public string? CellPhone { get; set; }
    public bool IsCellPhoneConfirmed { get; set; }

    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }

    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }

    public string? Picture { get; set; }
    public string? Location { get; set; }

    public int AccessFailedCount { get; set; }
    public bool TwoFactorEnabled { get; set; }

    public DateTime CreatedDateGregorian { get; set; }
    public string? CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }

    public bool IsSoftDeleted { get; set; }
    public DateTime? SoftDeleteDateGregorian { get; set; }
    public string? SoftDeleteDatePersian { get; set; }

    public bool IsActive { get; set; }

    public ICollection<string>? UserRoles { get; set; }
}

using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Aggregates.UserManagementAggregates;

public class OnlineShopUser : IdentityUser, IActiveEntity, ICreatedEntity, IModifiedEntity, ISoftDeletedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[]? Picture { get; set; }
    public string? Location { get; set; }

    public string NationalId { get; set; }
    public bool IsNationalIdConfirmed { get; set; }

    public bool IsActive { get; set; }

    public string CellPhone { get; set; }
    public bool IsCellPhoneConfirmed { get; set; }

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }

    public bool IsSoftDeleted { get; set; }
    public DateTime SoftDeleteDateGregorian { get; set; }
    public string? SoftDeleteDatePersian { get; set; }
}

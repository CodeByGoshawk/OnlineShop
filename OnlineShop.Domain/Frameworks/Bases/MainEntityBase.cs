using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Frameworks.Bases;

public abstract class MainEntityBase : IMainEntity
{
    public Guid Id { get ; set ; }
    public string? Code { get; set; }

    public DateTime CreatedDateGregorian {get;set;}
    public string? CreatedDatePersian {get;set;}

    public bool IsModified { get; set; }
    public DateTime? ModifyDateGregorian { get; set; }
    public string? ModifyDatePersian { get; set; }

    public bool IsSoftDeleted { get; set; }
    public DateTime? SoftDeleteDateGregorian { get ; set; }
    public string? SoftDeleteDatePersian { get ; set; }
}

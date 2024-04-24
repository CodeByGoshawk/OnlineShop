namespace OnlineShop.Domain.Frameworks.Abstracts;

public interface ISoftDeletedEntity
{
    bool IsSoftDeleted { get; set; }
    DateTime? SoftDeleteDateGregorian { get; set; }
    string? SoftDeleteDatePersian { get; set; }
}

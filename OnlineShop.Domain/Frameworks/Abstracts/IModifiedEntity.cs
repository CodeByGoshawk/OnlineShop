namespace OnlineShop.Domain.Frameworks.Abstracts;

public interface IModifiedEntity
{
    bool IsModified { get; set; }
    DateTime? ModifyDateGregorian { get; set; }
    string? ModifyDatePersian { get; set; }
}

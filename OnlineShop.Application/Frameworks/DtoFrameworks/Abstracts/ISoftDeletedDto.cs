namespace OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
public interface ISoftDeletedDto
{
    bool IsSoftDeleted { get; set; }
    DateTime SoftDeleteDateGregorian { get; set; }
    string SoftDeleteDatePersian { get; set; }
}

namespace OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
public interface IModifiedDto
{
    bool IsModified { get; set; }
    DateTime ModifyDateGregorian { get; set; }
    string ModifyDatePersian { get; set; }
}

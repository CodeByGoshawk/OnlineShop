namespace OnlineShop.Domain.Frameworks.Abstracts;

internal interface ICreatedEntity
{
    DateTime CreatedDateGregorian { get; set; }
    string CreatedDatePersian { get; set; }
}

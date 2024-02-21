namespace OnlineShop.Domain.Frameworks.Abstracts;

internal interface ICodedEntity<TCode>
{
    TCode Code { get; set; }
}

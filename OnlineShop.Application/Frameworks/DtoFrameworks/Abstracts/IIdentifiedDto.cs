namespace OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
public interface IIdentifiedDto<TKey>
{
    TKey Id { get; set; }
}

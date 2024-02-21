namespace OnlineShop.Domain.Frameworks.Abstracts;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
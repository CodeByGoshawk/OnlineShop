using OnlineShop.Domain.Frameworks.Abstracts;

namespace OnlineShop.Domain.Frameworks.Bases;

public class MainEntityBase : IMainEntity
{
    public Guid Id { get ; set ; }
    public string Code { get; set; }
}

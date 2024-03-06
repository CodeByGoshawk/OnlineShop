using OnlineShop.Application.Frameworks.DtoFrameworks.Abstracts;
using OnlineShop.Domain.Aggregates.SaleAggregates;

namespace OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductResultAppDto : IIdentifiedDto<Guid>, ICodedDto<string>, ITitledDto, ICreatedDto, IModifiedDto
{
    public Guid Id { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public decimal UnitPrice { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }

    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public bool IsModified { get; set; }
    public DateTime ModifyDateGregorian { get; set; }
    public string ModifyDatePersian { get; set; }
}

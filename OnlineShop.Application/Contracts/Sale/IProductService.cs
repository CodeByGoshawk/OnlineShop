using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;

namespace OnlineShop.Application.Contracts.Sale;
public interface IProductService : IService<GetProductAppDto, GetProductResultAppDto, GetAllProductsResultAppDto, PostProductAppDto, PutProductAppDto, DeleteProductAppDto, Guid>
{
}

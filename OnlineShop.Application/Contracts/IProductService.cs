using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;

namespace OnlineShop.Application.Contracts;
public interface IProductService : IService<GetProductResultAppDto, GetAllProductsResultAppDto, PostProductAppDto, PostProductResultAppDto, PutProductAppDto, PutProductResultAppDto, DeleteProductAppDto, DeleteProductResultAppDto, Guid>
{
}

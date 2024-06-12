using OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.Sale;
public interface IProductService
{
    Task<IResponse<GetProductsRangeResultAppDto>> GetAll();
    Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model);
}

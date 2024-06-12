using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.Sale;
public interface IProductService
{
    Task<IResponse<GetProductsRangeResultAppDto>> GetAllWithPrivateData();
    Task<IResponse<GetProductsRangeResultAppDto>> GetRangeBySeller(GetProductsRangeBySellerAppDto model);
    Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model);
    Task<IResponse<GetProductResultAppDto>> GetWithPrivateData(GetProductAppDto model);
    Task<IResponse<object>> Post(PostProductAppDto model);
    Task<IResponse<object>> Put(PutProductAppDto model);
    Task<IResponse<object>> Delete(DeleteProductAppDto model);
}

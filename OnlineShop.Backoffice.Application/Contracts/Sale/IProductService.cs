using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.Sale;
public interface IProductService
{
    Task<IResponse<GetProductsRangeWithPrivateDataResultAppDto>> GetAllWithPrivateData();
    Task<IResponse<GetProductsRangeResultAppDto>> GetRangeBySeller(GetProductsRangeBySellerAppDto model);
    Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model);
    Task<IResponse<GetProductWithPrivateDataResultAppDto>> GetWithPrivateData(GetProductAppDto model);
    Task<IResponse> Post(PostProductAppDto model);
    Task<IResponse> Put(PutProductAppDto model);
    Task<IResponse> Delete(DeleteProductAppDto model);
}

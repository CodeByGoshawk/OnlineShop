using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductCategoryDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.Sale;
public interface IProductCategoryService
{
    Task<IResponse<GetProductCategoryResultAppDto>> Get(GetProductCategoryAppDto model);
    Task<IResponse<GetAllProductCategoriesResultAppDto>> GetAll();
    Task<IResponse> Post(PostProductCategoryAppDto model);
    Task<IResponse> Put(PutProductCategoryAppDto model);
    Task<IResponse> Delete(DeleteProductCategoryAppDto model);
}

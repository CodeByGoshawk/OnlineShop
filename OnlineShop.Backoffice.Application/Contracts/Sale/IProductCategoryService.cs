using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductCategoryDtos;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Contracts.Sale;
public interface IProductCategoryService
{
    Task<IResponse<GetProductCategoryResultAppDto>> Get(GetProductCategoryAppDto model);
    Task<IResponse<GetAllProductCategoriesResultAppDto>> GetAll();
    Task<IResponse<object>> Post(PostProductCategoryAppDto model);
    Task<IResponse<object>> Put(PutProductCategoryAppDto model);
    Task<IResponse<object>> Delete(DeleteProductCategoryAppDto model);
}

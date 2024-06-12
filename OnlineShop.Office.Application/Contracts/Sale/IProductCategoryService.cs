using OnlineShop.Office.Application.Dtos.SaleDtos.ProductCategoryDtos;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Contracts.Sale;
public interface IProductCategoryService
{
    Task<IResponse<GetProductCategoryResultAppDto>> Get(GetProductCategoryAppDto model);
    Task<IResponse<GetAllProductCategoriesResultAppDto>> GetAll();
}

using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.ProductCategoryDtos;
using OnlineShop.RepositoryDesignPattern.Contracts;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Services.SaleServices;
public class ProductCategoryService(IProductCategoryRepository productCategoryRepository) : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<IResponse<GetProductCategoryResultAppDto>> Get(GetProductCategoryAppDto model)
    {
        var selectCategoryResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectCategoryResponse.IsSuccessful) return new Response<GetProductCategoryResultAppDto>(selectCategoryResponse.ErrorMessage!);
        var resultDto = new GetProductCategoryResultAppDto
        {
            Id = selectCategoryResponse.ResultModel!.Id,
            ParentId = selectCategoryResponse.ResultModel.ParentId,
            Title = selectCategoryResponse.ResultModel.Title!,
            Products = selectCategoryResponse.ResultModel.Products
        };
        return new Response<GetProductCategoryResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetAllProductCategoriesResultAppDto>> GetAll()
    {
        #region[Guards]
        var selectCategoriesResponse = await _productCategoryRepository.SelectAllAsync();
        if (!selectCategoriesResponse.IsSuccessful) return new Response<GetAllProductCategoriesResultAppDto>(selectCategoriesResponse.ErrorMessage!);
        #endregion

        var result = new GetAllProductCategoriesResultAppDto();

        foreach (var productCategory in selectCategoriesResponse.ResultModel!)
        {
            var getResultDto = new GetProductCategoryResultAppDto
            {
                Id = productCategory.Id,
                Title = productCategory.Title!,
                ParentId = productCategory.ParentId,
            };
            result.GetResultDtos.Add(getResultDto);
        }

        return new Response<GetAllProductCategoriesResultAppDto>(result);
    }
}
using OnlineShop.Backoffice.Application.Contracts.Sale;
using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductCategoryDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Services.SaleServices;
public class ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository) : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IResponse<GetProductCategoryResultAppDto>> Get(GetProductCategoryAppDto model)
    {
        var selectCategoryResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectCategoryResponse.IsSuccessful) return new Response<GetProductCategoryResultAppDto>(selectCategoryResponse.ErrorMessage!);
        var resultDto = new GetProductCategoryResultAppDto
        {
            Id = selectCategoryResponse.ResultModel!.Id,
            ParentId = selectCategoryResponse.ResultModel.ParentId,
            Title = selectCategoryResponse.ResultModel.Title!
        };
        return new Response<GetProductCategoryResultAppDto>(resultDto);
    } //Admin/Seller

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
    } // Admin/Seller

    public async Task<IResponse> Post(PostProductCategoryAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        if (model.Title is null) return new Response(MessageResource.Error_RequiredField);

        if (model.ParentId is not null and not 0)
        {
            var selectParentCategoryResponse = await _productCategoryRepository.SelectByIdAsync((int)model.ParentId);
            if (!selectParentCategoryResponse.IsSuccessful) return new Response(MessageResource.Error_ParentCategoryNotFound);
        }

        var newCategory = new ProductCategory
        {
            ParentId = model.ParentId != 0 ? model.ParentId : null,
            Title = model.Title,
        };

        var insertCategoryResponse = await _productCategoryRepository.InsertAsync(newCategory);

        if (insertCategoryResponse.IsSuccessful) await _productCategoryRepository.SaveAsync();

        return insertCategoryResponse.IsSuccessful ? new Response(model) : new Response(insertCategoryResponse.ErrorMessage!);
    } // Admin

    public async Task<IResponse> Put(PutProductCategoryAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        if (model.Title is null) return new Response(MessageResource.Error_RequiredField);
        if (model.Id == model.ParentId) return new Response(MessageResource.Error_ProductCategorySameIdandParentId);

        if (model.ParentId is not null and not 0)
        {
            var selectParentCategoryResponse = await _productCategoryRepository.SelectByIdAsync((int)model.ParentId);
            if (selectParentCategoryResponse.ResultModel is null) return new Response(MessageResource.Error_ParentCategoryNotFound);
        }

        var selectCategoryResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectCategoryResponse.IsSuccessful) return new Response(selectCategoryResponse.ErrorMessage!);

        var updatedCategory = selectCategoryResponse.ResultModel;
        updatedCategory!.ParentId = model.ParentId != 0 ? model.ParentId : null;
        updatedCategory.Title = model.Title;

        var updateCategoryResponse = await _productCategoryRepository.UpdateAsync(updatedCategory);

        if (updateCategoryResponse.IsSuccessful) await _productCategoryRepository.SaveAsync();

        return updateCategoryResponse.IsSuccessful ? new Response(model) : new Response(updateCategoryResponse.ErrorMessage!);
    } // Admin

    public async Task<IResponse> Delete(DeleteProductCategoryAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var selectCategoryResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectCategoryResponse.IsSuccessful) return new Response(selectCategoryResponse.ErrorMessage!);

        var deletedCategory = selectCategoryResponse.ResultModel;
        if (_productCategoryRepository.SelectAllAsync().Result.ResultModel!.Any(pc => pc.ParentId == model.Id)) return new Response(MessageResource.Error_ProductCategoryHasChild);
        if (_productRepository.SelectAllAsync().Result.ResultModel!.Any(p => p.ProductCategoryId == model.Id)) return new Response(MessageResource.Error_ProductCategoryHasProduct);
        var deleteCategoryResponse = await _productCategoryRepository.DeleteAsync(deletedCategory!);

        if (deleteCategoryResponse.IsSuccessful) await _productCategoryRepository.SaveAsync();

        return deleteCategoryResponse.IsSuccessful ? new Response(model) : new Response(deleteCategoryResponse.ErrorMessage!);
    } // Admin
}
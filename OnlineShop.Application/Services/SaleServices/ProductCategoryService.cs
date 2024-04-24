using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.Application.Services.SaleServices;
public class ProductCategoryService(IProductCategoryRepository productCategoryRepository,IProductRepository productRepository) : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly IProductRepository _productRepository = productRepository;

    // Get
    public async Task<IResponse<GetProductCategoryResultAppDto>> Get(GetProductCategoryAppDto model)
    {
        var selectOperationResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<GetProductCategoryResultAppDto>(selectOperationResponse.ErrorMessage!);
        var resultDto = new GetProductCategoryResultAppDto
        {
            Id = selectOperationResponse.ResultModel!.Id,
            ParentId = selectOperationResponse.ResultModel.ParentId,
            Title = selectOperationResponse.ResultModel.Title!
        };
        return new Response<GetProductCategoryResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetAllProductCategoriesResultAppDto>> GetAll()
    {
        var selectOperationResponse = await _productCategoryRepository.SelectAsync();
        if (!selectOperationResponse.IsSuccessful) return new Response<GetAllProductCategoriesResultAppDto>(selectOperationResponse.ErrorMessage!);

        var resultDto = new GetAllProductCategoriesResultAppDto();

        foreach (var productCategory in selectOperationResponse.ResultModel!)
        {
            var getResultDto = new GetProductCategoryResultAppDto
            {
                Id = productCategory.Id,
                Title = productCategory.Title!,
                ParentId = productCategory.ParentId,
            };
            resultDto.GetResultDtosList.Add(getResultDto);
        }

        return new Response<GetAllProductCategoriesResultAppDto>(resultDto);
    }

    // Post
    public async Task<IResponse<object>> Post(PostProductCategoryAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Title is null) return new Response<object>(MessageResource.Error_RequiredField);

        if ( model.ParentId is not null and not 0)
        {
            var selectParentOperationResponse = await _productCategoryRepository.SelectByIdAsync((int)model.ParentId);
            if(!selectParentOperationResponse.IsSuccessful) return new Response<object>(MessageResource.Error_ParentCategoryNotFound);
        }

        var newProductCategory = new ProductCategory
        {
            ParentId = model.ParentId != 0 ? model.ParentId : null,
            Title = model.Title,
        };

        var insertOperationResponse = await _productCategoryRepository.InsertAsync(newProductCategory);

        if (insertOperationResponse.IsSuccessful) await _productCategoryRepository.SaveAsync();

        return insertOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(insertOperationResponse.ErrorMessage!);
    }

    // Put
    public async Task<IResponse<object>> Put(PutProductCategoryAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Title is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.Id == model.ParentId) return new Response<object>(MessageResource.Error_ProductCategorySameIdandParentId);

        if (model.ParentId is not null and not 0)
        {
            var selectParentOperationResponse = await _productCategoryRepository.SelectByIdAsync((int)model.ParentId);
            if (selectParentOperationResponse.ResultModel is null) return new Response<object>(MessageResource.Error_ParentCategoryNotFound);
        }

        var selectOperationResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(selectOperationResponse.ErrorMessage!);

        var updatedProductCategory = selectOperationResponse.ResultModel;
        updatedProductCategory!.ParentId = model.ParentId != 0 ? model.ParentId : null;
        updatedProductCategory.Title = model.Title;

        var updateOperationResponse = await _productCategoryRepository.UpdateAsync(updatedProductCategory);

        if (updateOperationResponse.IsSuccessful) await _productCategoryRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOperationResponse.ErrorMessage!);
    }

    // Delete
    public async Task<IResponse<object>> Delete(DeleteProductCategoryAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var selectOperationResponse = await _productCategoryRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(selectOperationResponse.ErrorMessage!);

        var deletedProductCategory = selectOperationResponse.ResultModel;
        if (_productCategoryRepository.SelectAsync().Result.ResultModel!.Any(pc => pc.ParentId == model.Id)) return new Response<object>(MessageResource.Error_ProductCategoryHasChild);
        if (_productRepository.SelectAsync().Result.ResultModel!.Any(p => p.ProductCategoryId == model.Id)) return new Response<object>(MessageResource.Error_ProductCategoryHasProduct);
        var deleteOperationResponse = await _productCategoryRepository.DeleteAsync(deletedProductCategory!);

        if (deleteOperationResponse.IsSuccessful) await _productCategoryRepository.SaveAsync();

        return deleteOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(deleteOperationResponse.ErrorMessage!);
    }
}

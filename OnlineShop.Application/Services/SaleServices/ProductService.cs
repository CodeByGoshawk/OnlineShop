using OnlineShop.Application.Contracts.Sale;
using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Application.Services.SaleServices;
public class ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    // Get
    public async Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model)
    {
        if (model is null) return new Response<GetProductResultAppDto>(MessageResource.Error_NullInputModel);
        var selectOperationResponse = await _productRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<GetProductResultAppDto>(selectOperationResponse.ErrorMessage!);

        var resultDto = new GetProductResultAppDto
        {
            Id = selectOperationResponse.ResultModel!.Id,
            Code = selectOperationResponse.ResultModel.Code!,
            Title = selectOperationResponse.ResultModel.Title!,
            UnitPrice = selectOperationResponse.ResultModel.UnitPrice,
            CreatedDateGregorian = selectOperationResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectOperationResponse.ResultModel.CreatedDatePersian!,
            IsModified = selectOperationResponse.ResultModel.IsModified,
            ModifyDateGregorian = selectOperationResponse.ResultModel.ModifyDateGregorian,
            ModifyDatePersian = selectOperationResponse.ResultModel.ModifyDatePersian,
            ProductCategory = selectOperationResponse.ResultModel.ProductCategory
        };
        return new Response<GetProductResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetAllProductsResultAppDto>> GetAll()
    {
        var selectOperationResponse = await _productRepository.SelectAsync();
        if (!selectOperationResponse.IsSuccessful) return new Response<GetAllProductsResultAppDto>(selectOperationResponse.ErrorMessage!);

        var resultDto = new GetAllProductsResultAppDto();

        foreach (var product in selectOperationResponse.ResultModel!)
        {
            var getResultDto = new GetProductResultAppDto
            {
                Id = product.Id,
                Code = product.Code!,
                Title = product.Title!,
                UnitPrice = product.UnitPrice,
                CreatedDateGregorian = product.CreatedDateGregorian,
                CreatedDatePersian = product.CreatedDatePersian!,
                IsModified = product.IsModified,
                ModifyDateGregorian = product.ModifyDateGregorian,
                ModifyDatePersian = product.ModifyDatePersian,
                ProductCategory = product.ProductCategory
            };
            resultDto.GetResultDtos.Add(getResultDto);
        }

        return new Response<GetAllProductsResultAppDto>(resultDto);
    }

    // Post
    public async Task<IResponse<object>> Post(PostProductAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (string.IsNullOrEmpty(model.Code)) return new Response<object>(MessageResource.Error_RequiredField);
        if (string.IsNullOrEmpty(model.Title)) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.UnitPrice <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessUnitPrice);
        if (!_productCategoryRepository.SelectByIdAsync(model.ProductCategoryId).Result.IsSuccessful) return new Response<object>(MessageResource.Error_CategoryNotFound);

        if (_productRepository.SelectByCodeAsync(model.Code).Result.IsSuccessful) return new Response<object>(MessageResource.Error_RepetitiousCode);

        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Code = model.Code,
            Title = model.Title,
            UnitPrice = model.UnitPrice,
            ProductCategoryId = model.ProductCategoryId
        };

        var insertOperationResponse = await _productRepository.InsertAsync(newProduct);

        if (insertOperationResponse.IsSuccessful) await _productRepository.SaveAsync();

        return insertOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(insertOperationResponse.ErrorMessage!);
    }

    // Put
    public async Task<IResponse<object>> Put(PutProductAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Code is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.Title is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.UnitPrice <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessUnitPrice);
        if (!_productCategoryRepository.SelectByIdAsync(model.ProductCategoryId).Result.IsSuccessful) return new Response<object>(MessageResource.Error_CategoryNotFound);

        var selectByCodeOperationResponse = await _productRepository.SelectByCodeAsync(model.Code);
        if (selectByCodeOperationResponse.IsSuccessful && selectByCodeOperationResponse.ResultModel!.Id != model.Id)
            return new Response<object>(MessageResource.Error_RepetitiousCode);

        var selectOperationResponse = await _productRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(selectOperationResponse.ErrorMessage!);


        var updatedProduct = selectOperationResponse.ResultModel;
        updatedProduct!.Code = model.Code;
        updatedProduct.Title = model.Title;
        updatedProduct.UnitPrice = model.UnitPrice;
        updatedProduct.ProductCategoryId = model.ProductCategoryId;
        updatedProduct.IsModified = true;
        updatedProduct.ModifyDateGregorian = DateTime.Now;
        updatedProduct.ModifyDatePersian = DateTime.Now.ConvertToPersian();

        var updateOperationResponse = await _productRepository.UpdateAsync(updatedProduct);

        if (updateOperationResponse.IsSuccessful) await _productRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOperationResponse.ErrorMessage!);
    }

    // Delete
    public async Task<IResponse<object>> Delete(DeleteProductAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var selectOperationResponse = await _productRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(selectOperationResponse.ErrorMessage!);

        var deletedProduct = selectOperationResponse.ResultModel;
        deletedProduct!.IsSoftDeleted = true;
        deletedProduct.SoftDeleteDateGregorian = DateTime.Now;
        deletedProduct.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var updateOperationResponse = await _productRepository.UpdateAsync(deletedProduct);

        if (updateOperationResponse.IsSuccessful) await _productRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOperationResponse.ErrorMessage!);
    }
}
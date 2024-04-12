using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Application.Services.SaleServices;
public class ProductService(IProductRepository productRepository,IProductCategoryRepository productCategoryRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    // Get
    public async Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model)
    {
        var selectOperationResponse = await _productRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<GetProductResultAppDto>(selectOperationResponse.ErrorMessage!);
        var resultDto = new GetProductResultAppDto
        {
            Id = selectOperationResponse.Result.Id,
            Code = selectOperationResponse.Result.Code,
            Title = selectOperationResponse.Result.Title,
            UnitPrice = selectOperationResponse.Result.UnitPrice,
            CreatedDateGregorian = selectOperationResponse.Result.CreatedDateGregorian,
            CreatedDatePersian = selectOperationResponse.Result.CreatedDatePersian,
            IsModified = selectOperationResponse.Result.IsModified,
            ModifyDateGregorian = selectOperationResponse.Result.ModifyDateGregorian,
            ModifyDatePersian = selectOperationResponse.Result.ModifyDatePersian,
            ProductCategory = selectOperationResponse.Result.ProductCategory
        };
        return new Response<GetProductResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetAllProductsResultAppDto>> GetAll()
    {
        var selectOperationResponse = await _productRepository.SelectAsync();
        if (!selectOperationResponse.IsSuccessful) return new Response<GetAllProductsResultAppDto>(selectOperationResponse.ErrorMessage!);

        var resultDto = new GetAllProductsResultAppDto();

        foreach (var product in selectOperationResponse.Result!)
        {
            var getResultDto = new GetProductResultAppDto
            {
                Id = product.Id,
                Code = product.Code,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
                CreatedDateGregorian = product.CreatedDateGregorian,
                CreatedDatePersian = product.CreatedDatePersian,
                IsModified = product.IsModified,
                ModifyDateGregorian = product.ModifyDateGregorian,
                ModifyDatePersian = product.ModifyDatePersian,
                ProductCategory = product.ProductCategory
            };
            resultDto.GetResultDtosList.Add(getResultDto);
        }

        return new Response<GetAllProductsResultAppDto>(resultDto);
    }

    // Post
    public async Task<IResponse<object>> Post(PostProductAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Code is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.Title is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (!_productCategoryRepository.SelectByIdAsync(model.ProductCategoryId).Result.IsSuccessful) return new Response<object>(MessageResource.Error_CategoryNotFound);
        if (_productRepository.SelectByCodeAsync(model.Code).Result.IsSuccessful) return new Response<object>(MessageResource.Error_RepetitiousCode);

        var newProduct = new Product
        {
            Id = new Guid(),
            Code = model.Code,
            Title = model.Title,
            UnitPrice = model.UnitPrice,
            CreatedDateGregorian = DateTime.Now,
            CreatedDatePersian = Helper.GregorianToPersianDateConverter(DateTime.Now),
            IsModified = false,
            IsSoftDeleted = false,
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
        if (!_productCategoryRepository.SelectByIdAsync(model.ProductCategoryId).Result.IsSuccessful) return new Response<object>(MessageResource.Error_CategoryNotFound);

        var selectOperationResponse = await _productRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(selectOperationResponse.ErrorMessage!);
        var selectByCodeOperationResponse = await _productRepository.SelectByCodeAsync(model.Code);
        if (selectByCodeOperationResponse.IsSuccessful && selectByCodeOperationResponse.Result.Id != selectOperationResponse.Result.Id)
            return new Response<object>(MessageResource.Error_RepetitiousCode);

        var updatedProduct = selectOperationResponse.Result;
        updatedProduct.Code = model.Code;
        updatedProduct.Title = model.Title;
        updatedProduct.UnitPrice = model.UnitPrice;
        updatedProduct.ProductCategoryId = model.ProductCategoryId;
        updatedProduct.IsModified = true;
        updatedProduct.ModifyDateGregorian = DateTime.Now;
        updatedProduct.ModifyDatePersian = Helper.GregorianToPersianDateConverter(DateTime.Now);

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

        var deletedProduct = selectOperationResponse.Result;
        deletedProduct.IsSoftDeleted = true;
        deletedProduct.SoftDeleteDateGregorian = DateTime.Now;
        deletedProduct.SoftDeleteDatePersian = Helper.GregorianToPersianDateConverter(DateTime.Now);

        var updateOperationResponse = await _productRepository.UpdateAsync(deletedProduct);

        if (updateOperationResponse.IsSuccessful) await _productRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOperationResponse.ErrorMessage!);
    }
}
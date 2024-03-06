using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Application.Services.SaleServices;
public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    // Get
    public async Task<IResponse<GetProductResultAppDto>> Get(Guid id)
    {
        var selectOperationResponse = await _repository.SelectByIdAsync(id);
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
        var selectOperationResponse = await _repository.SelectAsync();
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
    public async Task<IResponse<PostProductResultAppDto>> Post(PostProductAppDto model)
    {
        if (model is null) return new Response<PostProductResultAppDto>(MessageResource.Error_NullInputModel);
        if (model.Code is null) return new Response<PostProductResultAppDto>(MessageResource.Error_RequiredField);
        if (model.Title is null) return new Response<PostProductResultAppDto>(MessageResource.Error_RequiredField);

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

        var insertOperationResponse = await _repository.InsertAsync(newProduct);

        return insertOperationResponse.IsSuccessful ? new Response<PostProductResultAppDto>(new PostProductResultAppDto()) : new Response<PostProductResultAppDto>(insertOperationResponse.ErrorMessage!);
    }

    // Put
    public async Task<IResponse<PutProductResultAppDto>> Put(PutProductAppDto model)
    {
        if (model is null) return new Response<PutProductResultAppDto>(MessageResource.Error_NullInputModel);
        if (model.Code is null) return new Response<PutProductResultAppDto>(MessageResource.Error_RequiredField);
        if (model.Title is null) return new Response<PutProductResultAppDto>(MessageResource.Error_RequiredField);

        var selectOperationResponse = await _repository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<PutProductResultAppDto>(selectOperationResponse.ErrorMessage!);

        var updatedProduct = selectOperationResponse.Result;
        updatedProduct.Code = model.Code;
        updatedProduct.Title = model.Title;
        updatedProduct.UnitPrice = model.UnitPrice;
        updatedProduct.ProductCategoryId = model.ProductCategoryId;
        updatedProduct.IsModified = true;
        updatedProduct.ModifyDateGregorian = DateTime.Now;
        updatedProduct.ModifyDatePersian = Helper.GregorianToPersianDateConverter(DateTime.Now);

        var updateOperationResponse = await _repository.UpdateAsync(updatedProduct);

        return updateOperationResponse.IsSuccessful ? new Response<PutProductResultAppDto>(new PutProductResultAppDto()) : new Response<PutProductResultAppDto>(updateOperationResponse.ErrorMessage!);
    }

    // Delete
    public async Task<IResponse<DeleteProductResultAppDto>> Delete(DeleteProductAppDto model)
    {
        if (model is null) return new Response<DeleteProductResultAppDto>(MessageResource.Error_NullInputModel);

        var selectOperationResponse = await _repository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<DeleteProductResultAppDto>(selectOperationResponse.ErrorMessage!);

        var deletedProduct = selectOperationResponse.Result;
        deletedProduct.IsSoftDeleted = true;
        deletedProduct.SoftDeleteDateGregorian = DateTime.Now;
        deletedProduct.SoftDeleteDatePersian = Helper.GregorianToPersianDateConverter(DateTime.Now);

        var deleteOperationResponse = await _repository.DeleteAsync(deletedProduct);

        return deleteOperationResponse.IsSuccessful ? new Response<DeleteProductResultAppDto>(new DeleteProductResultAppDto()) : new Response<DeleteProductResultAppDto>(deleteOperationResponse.ErrorMessage!);
    }
}
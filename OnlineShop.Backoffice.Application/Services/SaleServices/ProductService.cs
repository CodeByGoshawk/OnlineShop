using Microsoft.AspNetCore.Identity;
using OnlineShop.Backoffice.Application.Contracts.Sale;
using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Constants;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Services.SaleServices;
public class ProductService
    (
        IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository,
        UserManager<OnlineShopUser> userManager
    ) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly UserManager<OnlineShopUser> _userManager = userManager;

    public async Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model)
    {
        if (model is null) return new Response<GetProductResultAppDto>(MessageResource.Error_NullInputModel);
        var selectProductResponse = await _productRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectProductResponse.IsSuccessful || selectProductResponse.ResultModel!.SellerId != model.SellerId)
            return new Response<GetProductResultAppDto>(MessageResource.Error_UnauthorizedOwner);

        var result = new GetProductResultAppDto
        {
            Id = selectProductResponse.ResultModel!.Id,
            SellerId = selectProductResponse.ResultModel.SellerId,
            ProductCategoryId = selectProductResponse.ResultModel.ProductCategoryId,
            Code = selectProductResponse.ResultModel.Code!,
            Title = selectProductResponse.ResultModel.Title!,
            Picture = selectProductResponse.ResultModel.Picture,
            UnitPrice = selectProductResponse.ResultModel.UnitPrice,
            CreatedDateGregorian = selectProductResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectProductResponse.ResultModel.CreatedDatePersian!,
            ProductCategory = selectProductResponse.ResultModel.ProductCategory,
        };
        return new Response<GetProductResultAppDto>(result);
    }

    public async Task<IResponse<GetProductResultAppDto>> GetWithPrivateData(GetProductAppDto model)
    {
        if (model is null) return new Response<GetProductResultAppDto>(MessageResource.Error_NullInputModel);
        var selectProductResponse = await _productRepository.SelectByIdAsync(model.Id);
        if (!selectProductResponse.IsSuccessful) return new Response<GetProductResultAppDto>(selectProductResponse.ErrorMessage!);

        var result = new GetProductResultAppDto
        {
            Id = selectProductResponse.ResultModel!.Id,
            SellerId = selectProductResponse.ResultModel.SellerId,
            ProductCategoryId = selectProductResponse.ResultModel.ProductCategoryId,
            Code = selectProductResponse.ResultModel.Code!,
            Title = selectProductResponse.ResultModel.Title!,
            Picture = selectProductResponse.ResultModel.Picture,
            UnitPrice = selectProductResponse.ResultModel.UnitPrice,
            CreatedDateGregorian = selectProductResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectProductResponse.ResultModel.CreatedDatePersian!,
            IsModified = selectProductResponse.ResultModel.IsModified,
            ModifyDateGregorian = selectProductResponse.ResultModel.ModifyDateGregorian,
            ModifyDatePersian = selectProductResponse.ResultModel.ModifyDatePersian,
            ProductCategory = selectProductResponse.ResultModel.ProductCategory,
            IsSoftDeleted = selectProductResponse.ResultModel.IsSoftDeleted,
            SoftDeleteDateGregorian = selectProductResponse.ResultModel.SoftDeleteDateGregorian,
            SoftDeleteDatePersian = selectProductResponse.ResultModel.SoftDeleteDatePersian
        };
        return new Response<GetProductResultAppDto>(result);
    }

    public async Task<IResponse<GetProductsRangeResultAppDto>> GetAllWithPrivateData()
    {
        var selectProductResponse = await _productRepository.SelectAllAsync();
        if (!selectProductResponse.IsSuccessful) return new Response<GetProductsRangeResultAppDto>(selectProductResponse.ErrorMessage!);

        var result = new GetProductsRangeResultAppDto();

        selectProductResponse.ResultModel!.ToList().ForEach(product =>
        {
            var getResultDto = new GetProductResultAppDto
            {
                Id = product.Id,
                SellerId = product.SellerId,
                ProductCategoryId = product.ProductCategoryId,
                Code = product.Code!,
                Title = product.Title!,
                UnitPrice = product.UnitPrice,
                Picture = product.Picture,
                CreatedDateGregorian = product.CreatedDateGregorian,
                CreatedDatePersian = product.CreatedDatePersian!,
                IsModified = product.IsModified,
                ModifyDateGregorian = product.ModifyDateGregorian,
                ModifyDatePersian = product.ModifyDatePersian,
                ProductCategory = product.ProductCategory,
                IsSoftDeleted = product.IsSoftDeleted,
                SoftDeleteDateGregorian = product.SoftDeleteDateGregorian,
                SoftDeleteDatePersian = product.SoftDeleteDatePersian
            };
            result.GetResultDtos.Add(getResultDto);
        });

        return new Response<GetProductsRangeResultAppDto>(result);
    }

    public async Task<IResponse<GetProductsRangeResultAppDto>> GetRangeBySeller(GetProductsRangeBySellerAppDto model)
    {
        var selectProductResponse = await _productRepository.SelectNonDeletedsBySellerAsync(model.SellerId);
        if (!selectProductResponse.IsSuccessful) return new Response<GetProductsRangeResultAppDto>(selectProductResponse.ErrorMessage!);

        var result = new GetProductsRangeResultAppDto();

        selectProductResponse.ResultModel!.ToList().ForEach(product =>
        {
            var getResultDto = new GetProductResultAppDto
            {
                Id = product.Id,
                SellerId = product.SellerId,
                ProductCategoryId = product.ProductCategoryId,
                Code = product.Code!,
                Title = product.Title!,
                Picture = product.Picture,
                UnitPrice = product.UnitPrice,
                CreatedDateGregorian = product.CreatedDateGregorian,
                CreatedDatePersian = product.CreatedDatePersian!,
                ProductCategory = product.ProductCategory
            };
            result.GetResultDtos.Add(getResultDto);
        });

        return new Response<GetProductsRangeResultAppDto>(result);
    }

    public async Task<IResponse> Post(PostProductAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        if (model.UnitPrice <= 0) return new Response(MessageResource.Error_ZeroOrLessUnitPrice);
        if (!(await _productCategoryRepository.SelectByIdAsync(model.ProductCategoryId)).IsSuccessful) return new Response(MessageResource.Error_CategoryNotFound);

        var productSeller = await _userManager.FindByIdAsync(model.SellerId!);
        if (productSeller is null || productSeller.IsSoftDeleted) return new Response(MessageResource.Error_SellerNotFound);
        if (!await _userManager.IsInRoleAsync(productSeller, DatabaseConstants.DefaultRoles.SellerName)) return new Response(MessageResource.Error_WrongSeller);

        string productCode;
        while (true)
        {
            productCode = $"P{CodeMaker.MakeRandom()}";
            if (!_productRepository.SelectByCodeAsync(productCode).Result.IsSuccessful) break;
        }

        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            SellerId = model.SellerId,
            Code = productCode,
            Title = model.Title,
            UnitPrice = model.UnitPrice,
            Picture = model.Picture,
            ProductCategoryId = model.ProductCategoryId,
            CreatedDateGregorian = DateTime.Now,
            CreatedDatePersian = DateTime.Now.ConvertToPersian()
        };

        var insertProductResponse = await _productRepository.InsertAsync(newProduct);

        if (insertProductResponse.IsSuccessful) await _productRepository.SaveAsync();

        return insertProductResponse.IsSuccessful ? new Response(model) : new Response(insertProductResponse.ErrorMessage!);
    }

    public async Task<IResponse> Put(PutProductAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        if (model.UnitPrice <= 0) return new Response(MessageResource.Error_ZeroOrLessUnitPrice);
        if (!(await _productCategoryRepository.SelectByIdAsync(model.ProductCategoryId)).IsSuccessful) return new Response(MessageResource.Error_CategoryNotFound);
        var selectProductResponse = await _productRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectProductResponse.IsSuccessful || selectProductResponse.ResultModel!.SellerId != model.SellerId)
            return new Response(selectProductResponse.ErrorMessage!);
        #endregion

        var updatedProduct = selectProductResponse.ResultModel;
        updatedProduct!.Title = model.Title;
        updatedProduct.UnitPrice = model.UnitPrice;
        updatedProduct.Picture = model.Picture;
        updatedProduct.ProductCategoryId = model.ProductCategoryId;
        updatedProduct.IsModified = true;
        updatedProduct.ModifyDateGregorian = DateTime.Now;
        updatedProduct.ModifyDatePersian = DateTime.Now.ConvertToPersian();

        var updateProductResponse = await _productRepository.UpdateAsync(updatedProduct);

        if (updateProductResponse.IsSuccessful) await _productRepository.SaveAsync();

        return updateProductResponse.IsSuccessful ? new Response(model) : new Response(updateProductResponse.ErrorMessage!);
    }

    public async Task<IResponse> Delete(DeleteProductAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        var selectProductResponse = await _productRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectProductResponse.IsSuccessful && (await IsAnyTypeOfAdminsAsync(model.DeleterUserId!)).IsSuccessful)
            return new Response(selectProductResponse.ErrorMessage!);
        else if (!selectProductResponse.IsSuccessful || selectProductResponse.ResultModel!.SellerId != model.DeleterUserId)
            return new Response(MessageResource.Error_UnauthorizedOwner);
        #endregion

        var deletedProduct = selectProductResponse.ResultModel;
        deletedProduct!.IsSoftDeleted = true;
        deletedProduct.SoftDeleteDateGregorian = DateTime.Now;
        deletedProduct.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var updateOperationResponse = await _productRepository.UpdateAsync(deletedProduct);

        if (updateOperationResponse.IsSuccessful) await _productRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response(model) : new Response(updateOperationResponse.ErrorMessage!);
    }


    //--------------------------------------------[Private Methods]--------------------------------------------//

    private async Task<IResponse> IsAnyTypeOfAdminsAsync(string userId)
    {
        List<string> adminRoles =
        [
            DatabaseConstants.DefaultRoles.GodAdminName,
            DatabaseConstants.DefaultRoles.AdminName
        ];
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return new Response(MessageResource.Error_UserNotFound);

        var userRoles = await _userManager.GetRolesAsync(user);
        return new Response(userRoles.Any(role => adminRoles.Contains(role)));
    }
}
﻿using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Services.SaleServices;
public class ProductService
    (
        IProductRepository productRepository
    ) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IResponse<GetProductsRangeResultAppDto>> GetAll()
    {
        var selectProductResponse = await _productRepository.SelectAllNonDeletedsAsync();
        if (!selectProductResponse.IsSuccessful) return new Response<GetProductsRangeResultAppDto>(selectProductResponse.ErrorMessage!);

        var result = new GetProductsRangeResultAppDto();

        selectProductResponse.ResultModel!.ToList().ForEach(product =>
        {
            var getResultDto = new GetProductResultAppDto
            {
                Id = product.Id,
                ProductCategoryId = product.ProductCategoryId,
                Code = product.Code!,
                Title = product.Title!,
                UnitPrice = product.UnitPrice,
                Picture = product.Picture
            };
            result.GetResultDtos.Add(getResultDto);
        });

        return new Response<GetProductsRangeResultAppDto>(result);
    }

    public async Task<IResponse<GetProductResultAppDto>> Get(GetProductAppDto model)
    {
        if (model is null) return new Response<GetProductResultAppDto>(MessageResource.Error_NullInputModel);
        var selectProductResponse = await _productRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectProductResponse.IsSuccessful) return new Response<GetProductResultAppDto>(selectProductResponse.ErrorMessage!);

        var result = new GetProductResultAppDto
        {
            Id = selectProductResponse.ResultModel!.Id,
            ProductCategoryId = selectProductResponse.ResultModel.ProductCategoryId,
            Code = selectProductResponse.ResultModel.Code!,
            Title = selectProductResponse.ResultModel.Title!,
            UnitPrice = selectProductResponse.ResultModel.UnitPrice,
            Picture = selectProductResponse.ResultModel.Picture
        };
        return new Response<GetProductResultAppDto>(result);
    }
}
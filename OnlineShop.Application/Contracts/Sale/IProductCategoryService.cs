﻿using OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
using OnlineShop.Application.Frameworks.ServiceFrameworks.Abstracts;

namespace OnlineShop.Application.Contracts.Sale;
public interface IProductCategoryService : IService<GetProductCategoryAppDto, GetProductCategoryResultAppDto, GetAllProductCategoriesResultAppDto, PostProductCategoryAppDto, PutProductCategoryAppDto, DeleteProductCategoryAppDto, int>
{
}

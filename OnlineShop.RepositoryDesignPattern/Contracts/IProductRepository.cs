﻿using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Frameworks.Abstracts;
using ResponseFramewrok;

namespace OnlineShop.RepositoryDesignPattern.Contracts;
public interface IProductRepository : IRepository<Product,Guid>
{
    public Task<IResponse<Product>> SelectByCodeAsync(string code);
}

﻿using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductResultAppDto
{
    public Guid Id { get; set; }
    public int ProductCategoryId { get; set; }
    [RequesterId]
    public string? SellerId { get; set; }

    public decimal UnitPrice { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string? Picture { get; set; }
    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }

    public ProductCategory? ProductCategory { get; set; }
}

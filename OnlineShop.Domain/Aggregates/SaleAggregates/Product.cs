﻿using OnlineShop.Domain.Frameworks.Abstracts;
using OnlineShop.Domain.Frameworks.Bases;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class Product : MainEntityBase , ITitledEntity
{
    // Keys
    public ProductCategory ProductCategory { get; set; }

    // Properties
    public string Title { get; set; }
    public decimal UnitPrice { get; set; }
}
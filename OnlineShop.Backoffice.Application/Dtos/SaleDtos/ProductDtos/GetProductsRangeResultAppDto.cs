﻿using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductsRangeResultAppDto
{
    [JsonPropertyName("Products")]
    public List<GetProductResultAppDto> GetResultDtos { get; set; } = [];
}

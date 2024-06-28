using System.Text.Json.Serialization;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductsRangeResultAppDto
{
    [JsonPropertyName("ProductCategories")]
    public List<GetProductResultAppDto> GetResultDtos { get; set; } = [];
}

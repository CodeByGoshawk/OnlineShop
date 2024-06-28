using System.Text.Json.Serialization;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class GetAllProductCategoriesResultAppDto
{
    [JsonPropertyName("Products")]
    public List<GetProductCategoryResultAppDto> GetResultDtos { get; set; } = [];
}

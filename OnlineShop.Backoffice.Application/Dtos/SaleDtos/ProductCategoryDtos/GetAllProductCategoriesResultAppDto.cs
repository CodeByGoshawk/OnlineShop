using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class GetAllProductCategoriesResultAppDto
{
    [JsonPropertyName("ProductCategories")]
    public List<GetProductCategoryResultAppDto> GetResultDtos { get; set; } = [];
}

using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductsRangeWithPrivateDataResultAppDto
{
    [JsonPropertyName("Products")]
    public List<GetProductWithPrivateDataResultAppDto> GetResultDtos { get; set; } = [];
}

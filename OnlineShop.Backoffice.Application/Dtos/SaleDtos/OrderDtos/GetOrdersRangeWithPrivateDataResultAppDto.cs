using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrdersRangeWithPrivateDataResultAppDto
{
    [JsonPropertyName("Orders")]
    public List<GetOrderWithPrivateDataResultAppDto> GetResultDtos { get; set; } = [];
}

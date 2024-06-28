using System.Text.Json.Serialization;

namespace OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
public class GetOrdersRangeResultAppDto
{
    [JsonPropertyName("Orders")]
    public List<GetOrderResultAppDto> GetResultDtos { get; set; } = [];
}

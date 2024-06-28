using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetAllUsersResultAppDto
{
    [JsonPropertyName("Users")]
    public List<GetUserResultAppDto> GetResultDtos { get; set; } = [];
}

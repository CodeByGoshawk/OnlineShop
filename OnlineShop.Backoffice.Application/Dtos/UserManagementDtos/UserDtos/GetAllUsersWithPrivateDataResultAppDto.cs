using System.Text.Json.Serialization;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class GetAllUsersWithPrivateDataResultAppDto
{
    [JsonPropertyName("UsersWithPrivateData")]
    public List<GetUserWithPrivateDataResultAppDto> GetResultDtos { get; set; } = [];
}

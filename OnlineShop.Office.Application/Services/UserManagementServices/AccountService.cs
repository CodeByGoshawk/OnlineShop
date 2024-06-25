using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Office.Application.Contracts.UserManagement;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.AccountDtos;
using PublicTools.Resources;
using ResponseFramewrok;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShop.Office.Application.Services.UserManagementServices;
public class AccountService(UserManager<OnlineShopUser> userManager, IConfiguration configuration) : IAccountService
{
    private readonly UserManager<OnlineShopUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IResponse<LoginResultAppDto>> Login(LoginAppDto model)
    {
        if (model is null) return new Response<LoginResultAppDto>(MessageResource.Error_NullInputModel);

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null || user.IsSoftDeleted || !await _userManager.CheckPasswordAsync(user, model.Password)) return new Response<LoginResultAppDto>(MessageResource.Error_AuthenticationFailed);

        List<Claim> authenticationClaims = [new Claim(ClaimTypes.Sid, user.Id!)];
        _userManager.GetRolesAsync(user).Result.ToList().ForEach(role => authenticationClaims.Add(new(ClaimTypes.Role, role)));

        var result = new LoginResultAppDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(GetToken(authenticationClaims))
        };

        return new Response<LoginResultAppDto>(result);
    }

    private JwtSecurityToken GetToken(List<Claim> claims)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:ValidIssuer"]!,
                audience: _configuration["JWT:ValidAudience"]!,
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials: signingCredentials
            );
        return token;
    }
}

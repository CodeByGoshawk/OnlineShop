using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.AccountDtos;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using PublicTools.Resources;
using ResponseFramewrok;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShop.Backoffice.Application.Services.UserManagementServices;
public class AccountService(UserManager<OnlineShopUser> userManager, IConfiguration configuration) : IAccountService
{
    private readonly UserManager<OnlineShopUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IResponse<LoginResultAppDto>> Login(LoginAppDto model)
    {
        if (model is null) return new Response<LoginResultAppDto>(MessageResource.Error_NullInputModel);

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null || user.IsSoftDeleted || !await _userManager.CheckPasswordAsync(user, model.Password)) return new Response<LoginResultAppDto>(MessageResource.Error_AuthenticationFailed);

        List<Claim> authenticationClaims = [new(ClaimTypes.Sid, user.Id!)];

        _userManager.GetRolesAsync(user).Result
            .ToList()
            .ForEach(role => authenticationClaims.Add(new(ClaimTypes.Role, role)));

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
                expires: DateTime.Now.AddHours(1),
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                signingCredentials: signingCredentials
            );
        return token;
    }
}

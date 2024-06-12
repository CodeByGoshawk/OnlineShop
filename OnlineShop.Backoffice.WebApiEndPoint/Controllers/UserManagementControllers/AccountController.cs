using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.AccountDtos;
using PublicTools.Resources;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.UserManagementControllers;

[ApiController]
[Route("api/Account")]
public class AccountController(IAccountService accountService) : Controller
{
    private readonly IAccountService _accountService = accountService;

    [HttpGet("Login")]
    public async Task<IActionResult> Get([FromBody] LoginAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _accountService.Login(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Unauthorized(getOperationResponse.ErrorMessage);
    }
}

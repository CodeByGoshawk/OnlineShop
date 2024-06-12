using Microsoft.AspNetCore.Mvc;
using OnlineShop.Office.Application.Contracts.UserManagement;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.AccountDtos;
using PublicTools.Resources;

namespace OnlineShop.Office.WebApiEndPoint.Controllers.UserManagementControllers;

[ApiController]
[Route("api/Account")]
public class AccountController(IAccountService accountService) : Controller
{
    private readonly IAccountService _accountService = accountService;

    [HttpGet("SignIn")]
    public async Task<IActionResult> Get([FromBody] LoginAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _accountService.Login(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Unauthorized(getOperationResponse.ErrorMessage);
    }
}

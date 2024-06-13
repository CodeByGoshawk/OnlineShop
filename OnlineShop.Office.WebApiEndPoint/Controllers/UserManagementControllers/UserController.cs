using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Office.Application.Contracts.UserManagement;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using PublicTools.Constants;
using PublicTools.Resources;

namespace OnlineShop.Office.WebApiEndPoint.Controllers.UserManagementControllers;

[ApiController]
[Route("api/User")]
public class UserController(IUserService userService, IAuthorizationService authorizationService) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [Authorize]
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var getOperationResponse = await _userService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> Post([FromBody] PostOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var postOperationResponse = await _userService.Post(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("EditProfile")]
    public async Task<IActionResult> Put([FromBody] PutOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var postOperationResponse = await _userService.Put(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var postOperationResponse = await _userService.ChangePassword(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var postOperationResponse = await _userService.Delete(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }
}

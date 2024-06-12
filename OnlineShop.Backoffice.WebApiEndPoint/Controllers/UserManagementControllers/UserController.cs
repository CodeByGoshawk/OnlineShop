using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Handlers;
using PublicTools.Constants;
using PublicTools.Resources;
using System.Security.Claims;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.UserManagementControllers;

[ApiController]
[Route("api/User")]
public class UserController(IUserService userService , IAuthorizationService authorizationService) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [Authorize(Roles = "Seller")]
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var getOperationResponse = await _userService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetNonAdminUserWithPrivateData")]
    public async Task<IActionResult> GetNonAdminWithPrivateData([FromBody] GetOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _userService.GetNonAdminWithPrivateData(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "GodAdmin")]
    [HttpGet("GetWithPrivateData")]
    public async Task<IActionResult> GetWithPrivateData([FromBody] GetOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _userService.GetWithPrivateData(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetNonAdminUsersWithPrivateData")]
    public async Task<IActionResult> GetNonAdminsWithPrivateData()
    {
        var getAllOperationResponse = await _userService.GetNonAdminsWithPrivateData();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [Authorize(Roles = "GodAdmin")]
    [HttpGet("GetAllWithPrivateData")]
    public async Task<IActionResult> GetAllWithPrivateData()
    {
        var getAllOperationResponse = await _userService.GetAllWithPrivateData();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.Register(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("EditProfile")]
    public async Task<IActionResult> EditSelf([FromBody] EditOnlineShopUserPropertiesAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User,model,PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);
;
        var postOperationResponse = await _userService.EditSelf(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "GodAdmin")]
    [HttpPut("EditNonGodAdminUser")]
    public async Task<IActionResult> EditNonGodAdmin([FromBody] EditOnlineShopUserPropertiesAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.EditNonGodAdmin(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("EditNonAdminUser")]
    public async Task<IActionResult> EditNonAdmin([FromBody] EditOnlineShopUserPropertiesAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.EditNonAdmin(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("ChangeUserPassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var postOperationResponse = await _userService.ChangePassword(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "GodAdmin")]
    [HttpPut("DeleteNonGodAdminUser")]
    public async Task<IActionResult> DeleteNonGodAdmin([FromBody] DeleteOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.DeleteNonGodAdmin(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("DeleteNonAdminUser")]
    public async Task<IActionResult> DeleteNonAdmin([FromBody] DeleteOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.DeleteNonAdmin(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "GodAdmin")]
    [HttpPut("GrantOrRevokeAdminRole")]
    public async Task<IActionResult> GrantOrRevokeAdminRole([FromBody] GrantOrRevokeAdminRoleAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.GrantOrRevokeAdminRole(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }
}

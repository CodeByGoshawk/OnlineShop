using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using PublicTools.Attributes;
using PublicTools.Constants;
using PublicTools.Resources;
using System.Security.Claims;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.UserManagementControllers;

[ApiController]
[Route("api/User")]
public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    [Authorize]
    [HttpGet("GetSelf")]
    public async Task<IActionResult> Get()
    {
        var model = new GetUserAppDto();
        SetModelRequesterId(model);

        var getOperationResponse = await _userService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpGet("GetWithPrivateData")]
    public async Task<IActionResult> GetWithPrivateData([FromBody] GetUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var getOperationResponse = await _userService.GetWithPrivateData(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpGet("GetAllWithPrivateData")]
    public async Task<IActionResult> GetAllWithPrivateData()
    {
        var model = new GetAllUsersAppDto();
        SetModelRequesterId(model);

        var getAllOperationResponse = await _userService.GetAllWithPrivateData(model);
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel!.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.Register(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("Edit")]
    public async Task<IActionResult> Edit([FromBody] EditUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var postOperationResponse = await _userService.Edit(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var postOperationResponse = await _userService.ChangePassword(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = DatabaseConstants.DefaultRoles.GodAdminName)]
    [HttpPut("GrantOrRevokeAdminRole")]
    public async Task<IActionResult> GrantOrRevokeAdminRole([FromBody] GrantOrRevokeRoleAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.GrantOrRevokeAdminRole(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpPut("GrantOrRevokeSellerRole")]
    public async Task<IActionResult> GrantOrRevokeSellerRole([FromBody] GrantOrRevokeRoleAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _userService.GrantOrRevokeSellerRole(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var postOperationResponse = await _userService.Delete(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    private void SetModelRequesterId(object model)
    {
        var modelOwnerIdProperty = model.GetType().GetProperties().SingleOrDefault(p => p.IsDefined(typeof(RequesterIdAttribute), false));

        if (modelOwnerIdProperty is null) return;

        modelOwnerIdProperty.SetValue(model, User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)!.Value);
    }
}

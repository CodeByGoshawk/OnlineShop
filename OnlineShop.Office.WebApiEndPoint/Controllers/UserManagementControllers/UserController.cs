using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Office.Application.Contracts.UserManagement;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using PublicTools.Attributes;
using PublicTools.Resources;
using System.Security.Claims;

namespace OnlineShop.Office.WebApiEndPoint.Controllers.UserManagementControllers;

[ApiController]
[Route("api/User")]
public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    [Authorize]
    [HttpGet("GetSelf")]
    public async Task<IActionResult> Get()
    {
        var model = new GetOnlineShopUserAppDto();
        SetModelRequesterId(model);

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

        SetModelRequesterId(model);

        var postOperationResponse = await _userService.Put(model);
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

    [Authorize]
    [HttpDelete("DeleteSelf")]
    public async Task<IActionResult> Delete()
    {
        var model = new DeleteOnlineShopUserAppDto();
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

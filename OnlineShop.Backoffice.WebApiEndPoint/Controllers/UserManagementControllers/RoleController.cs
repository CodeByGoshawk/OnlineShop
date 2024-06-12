using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.RoleDtos;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using PublicTools.Resources;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.UserManagementControllers;
[Authorize(Roles = "GodAdmin")]
[ApiController]
[Route("api/Role")]
public class RoleController(IRoleService roleService) : Controller
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var getAllOperationResponse = await _roleService.GetAll();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetOnlineShopRoleAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _roleService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] RegisterOnlineShopRoleAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _roleService.Register(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> Put([FromBody] EditOnlineShopRoleAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var putOperationResponse = await _roleService.Edit(model);
        return putOperationResponse.IsSuccessful ? Ok(putOperationResponse.Message) : Problem(putOperationResponse.ErrorMessage, statusCode: (int)putOperationResponse.HttpStatusCode);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteOnlineShopUserAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var deleteOperationResponse = await _roleService.Delete(model);
        return deleteOperationResponse.IsSuccessful ? Ok(deleteOperationResponse.Message) : Problem(deleteOperationResponse.ErrorMessage, statusCode: (int)deleteOperationResponse.HttpStatusCode);
    }
}

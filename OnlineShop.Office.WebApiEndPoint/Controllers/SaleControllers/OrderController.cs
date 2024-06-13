using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Handlers;
using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
using PublicTools.Constants;
using PublicTools.Resources;

namespace OnlineShop.Office.WebApiEndPoint.Controllers.SaleControllers;
[ApiController]
[Route("api/Order")]
public class OrderController(IOrderService orderService, IAuthorizationService authorizationService) : Controller
{
    private readonly IOrderService _orderService = orderService;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [Authorize]
    [HttpGet("GetBuyerOrders")]
    public async Task<IActionResult> GetRangeByBuyer(GetOrdersRangeByBuyerAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var getOrdersResponse = await _orderService.GetRangeByBuyer(model);
        return getOrdersResponse.IsSuccessful ? Ok(getOrdersResponse.ResultModel!.GetResultDtos) : Problem(getOrdersResponse.ErrorMessage, statusCode: (int)getOrdersResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var resource = new OwnerOnlyResource(_orderService, model.Id);
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, resource, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var getOrderResponse = await _orderService.Get(model);
        return getOrderResponse.IsSuccessful ? Ok(getOrderResponse.ResultModel) : Problem(getOrderResponse.ErrorMessage, statusCode: (int)getOrderResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] PostOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, model, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var postOrderResponse = await _orderService.Post(model);
        return postOrderResponse.IsSuccessful ? Ok(postOrderResponse.Message) : Problem(postOrderResponse.ErrorMessage, statusCode: (int)postOrderResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("Put")]
    public async Task<IActionResult> Put([FromBody] PutOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var resource = new OwnerOnlyResource(_orderService, model.Id);
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, resource, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var putOrderResponse = await _orderService.Put(model);
        return putOrderResponse.IsSuccessful ? Ok(putOrderResponse.Message) : Problem(putOrderResponse.ErrorMessage, statusCode: (int)putOrderResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var resource = new OwnerOnlyResource(_orderService, model.Id);
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, resource, PolicyConstants.OwnerOnlyPolicy);
        if (!authorizationResult.Succeeded) return Forbid(MessageResource.Error_UnauthorizedOwner);

        var deleteOrderResponse = await _orderService.Delete(model);
        return deleteOrderResponse.IsSuccessful ? Ok(deleteOrderResponse.Message) : Problem(deleteOrderResponse.ErrorMessage, statusCode: (int)deleteOrderResponse.HttpStatusCode);
    }
}

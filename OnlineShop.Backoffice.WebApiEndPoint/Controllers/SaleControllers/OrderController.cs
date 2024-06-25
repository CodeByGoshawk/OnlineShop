using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.Application.Contracts.Sale;
using OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
using PublicTools.Attributes;
using PublicTools.Constants;
using PublicTools.Resources;
using System.Security.Claims;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.SaleControllers;
[ApiController]
[Route("api/Order")]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpGet("GetAllWithPrivateData")]
    public async Task<IActionResult> GetAllWithPrivateData()
    {
        var getAllOperationResponse = await _orderService.GetAllWithPrivateData();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel!.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [Authorize(Roles = "Seller")]
    [HttpGet("GetSelfOrders")]
    public async Task<IActionResult> GetRangeBySeller()
    {
        var model = new GetOrdersRangeBySellerAppDto();
        SetModelRequesterId(model);

        var getOperationResponse = await _orderService.GetRangeBySeller(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpGet("GetWithPrivateData")]
    public async Task<IActionResult> GetWithPrivateData([FromBody] GetOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _orderService.GetWithPrivateData(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Seller")]
    [HttpGet("Get")]
    public async Task<IActionResult> GetWithSellerOrderDetails([FromBody] GetOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var getOperationResponse = await _orderService.GetWithSellerOrderDetails(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _orderService.Delete(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    private void SetModelRequesterId(object model)
    {
        var modelOwnerIdProperty = model.GetType().GetProperties().SingleOrDefault(p => p.IsDefined(typeof(RequesterIdAttribute), false));

        if (modelOwnerIdProperty is null) return;

        modelOwnerIdProperty.SetValue(model, User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)!.Value);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
using PublicTools.Resources;

namespace OnlineShop.Office.WebApiEndPoint.Controllers.SaleControllers;
[ApiController]
[Route("api/Order")]
public class OrderController(IOrderService orderHeaderService) : Controller
{
    private readonly IOrderService _orderHeaderService = orderHeaderService;

    [Authorize]
    [HttpGet("GetBuyerOrders")]
    public async Task<IActionResult> GetRangeByBuyer(GetOrdersRangeByBuyerAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOrdersResponse = await _orderHeaderService.GetRangeByBuyer(model);
        return getOrdersResponse.IsSuccessful ? Ok(getOrdersResponse.ResultModel!.GetResultDtos) : Problem(getOrdersResponse.ErrorMessage, statusCode: (int)getOrdersResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOrderResponse = await _orderHeaderService.Get(model);
        return getOrderResponse.IsSuccessful ? Ok(getOrderResponse.ResultModel) : Problem(getOrderResponse.ErrorMessage, statusCode: (int)getOrderResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] PostOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOrderResponse = await _orderHeaderService.Post(model);
        return postOrderResponse.IsSuccessful ? Ok(postOrderResponse.Message) : Problem(postOrderResponse.ErrorMessage, statusCode: (int)postOrderResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpPut("Put")]
    public async Task<IActionResult> Put([FromBody] PutOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var putOrderResponse = await _orderHeaderService.Put(model);
        return putOrderResponse.IsSuccessful ? Ok(putOrderResponse.Message) : Problem(putOrderResponse.ErrorMessage, statusCode: (int)putOrderResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var deleteOrderResponse = await _orderHeaderService.Delete(model);
        return deleteOrderResponse.IsSuccessful ? Ok(deleteOrderResponse.Message) : Problem(deleteOrderResponse.ErrorMessage, statusCode: (int)deleteOrderResponse.HttpStatusCode);
    }
}

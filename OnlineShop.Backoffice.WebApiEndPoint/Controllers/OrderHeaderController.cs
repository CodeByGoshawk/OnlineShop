using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.OrderHeaderDtos;
using PublicTools.Resources;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers;
[ApiController]
[Route("api/Order")]
public class OrderHeaderController(IOrderService orderHeaderService) : Controller
{
    private readonly IOrderService _orderHeaderService = orderHeaderService;

    [HttpGet("GetAllOrders")]
    public async Task<IActionResult> GetAll()
    {
        var getAllOperationResponse = await _orderHeaderService.GetAll();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [HttpGet("GetOrder")]
    public async Task<IActionResult> Get([FromBody] GetOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _orderHeaderService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [HttpPost("PostOrder")]
    public async Task<IActionResult> Post([FromBody] PostOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _orderHeaderService.Post(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [HttpPut("PutOrder")]
    public async Task<IActionResult> Put([FromBody] PutOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _orderHeaderService.Put(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [HttpDelete("DeleteOrder")]
    public async Task<IActionResult> Delete([FromBody] DeleteOrderAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _orderHeaderService.Delete(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }
}

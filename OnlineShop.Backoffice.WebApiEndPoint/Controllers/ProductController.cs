using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.ProductDtos;
using PublicTools.Resources;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers;
[ApiController]
[Route("api/Product")]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAll()
    {
        var getAllOperationResponse = await _productService.GetAll();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtosList) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [HttpGet("GetProduct")]
    public async Task<IActionResult> Get([FromBody] GetProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _productService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [HttpPost("PostProduct")]
    public async Task<IActionResult> Post([FromBody] PostProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _productService.Post(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [HttpPut("PutProduct")]
    public async Task<IActionResult> Put([FromBody] PutProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var putOperationResponse = await _productService.Put(model);
        return putOperationResponse.IsSuccessful ? Ok(putOperationResponse.Message) : Problem(putOperationResponse.ErrorMessage, statusCode: (int)putOperationResponse.HttpStatusCode);
    }

    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var deleteOperationResponse = await _productService.Delete(model);
        return deleteOperationResponse.IsSuccessful ? Ok(deleteOperationResponse.Message) : Problem(deleteOperationResponse.ErrorMessage, statusCode: (int)deleteOperationResponse.HttpStatusCode);
    }
}

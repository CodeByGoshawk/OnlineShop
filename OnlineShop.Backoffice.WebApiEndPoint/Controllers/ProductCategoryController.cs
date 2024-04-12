using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.ProductCategoryDtos;
using PublicTools.Resources;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers;

[ApiController]
[Route("api/ProductCategory")]
public class ProductCategoryController(IProductCategoryService service) : Controller
{
    private readonly IProductCategoryService _service = service;

    [HttpGet("GetAllProductCategories")]
    public async Task<IActionResult> GetAll()
    {
        var getAllOperationResponse = await _service.GetAll();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.Result.GetResultDtosList) : Problem(getAllOperationResponse.ErrorMessage, statusCode: (int)getAllOperationResponse.HttpStatusCode);
    }

    [HttpGet("GetProductCategory")]
    public async Task<IActionResult> Get([FromBody] GetProductCategoryAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _service.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.Result) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [HttpPost("PostProductCategory")]
    public async Task<IActionResult> Post([FromBody] PostProductCategoryAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var postOperationResponse = await _service.Post(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [HttpPut("PutProductCategory")]
    public async Task<IActionResult> Put([FromBody] PutProductCategoryAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var putOperationResponse = await _service.Put(model);
        return putOperationResponse.IsSuccessful ? Ok(putOperationResponse.Message) : Problem(putOperationResponse.ErrorMessage, statusCode: (int)putOperationResponse.HttpStatusCode);
    }

    [HttpDelete("DeleteProductCategory")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCategoryAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var deleteOperationResponse = await _service.Delete(model);
        return deleteOperationResponse.IsSuccessful ? Ok(deleteOperationResponse.Message) : Problem(deleteOperationResponse.ErrorMessage, statusCode: (int)deleteOperationResponse.HttpStatusCode);
    }
}

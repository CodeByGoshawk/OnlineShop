using Microsoft.AspNetCore.Mvc;
using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.ProductDtos;
using PublicTools.Resources;

namespace OnlineShop.Office.WebApiEndPoint.Controllers.SaleControllers;
[ApiController]
[Route("api/Product")]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var getAllOperationResponse = await _productService.GetAll();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: (int)getAllOperationResponse.HttpStatusCode);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        var getOperationResponse = await _productService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }
}

using Microsoft.AspNetCore.Mvc;
using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.ProductCategoryDtos;
using PublicTools.Resources;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.SaleControllers;

[ApiController]
[Route("api/ProductCategory")]
public class ProductCategoryController(IProductCategoryService productCategoryService) : Controller
{
    private readonly IProductCategoryService _productCategoryService = productCategoryService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var getAllOperationResponse = await _productCategoryService.GetAll();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: (int)getAllOperationResponse.HttpStatusCode);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetProductCategoryAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _productCategoryService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }
}

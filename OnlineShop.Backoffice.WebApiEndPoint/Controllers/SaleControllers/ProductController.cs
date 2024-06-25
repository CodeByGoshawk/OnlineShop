using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Backoffice.Application.Contracts.Sale;
using OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
using PublicTools.Attributes;
using PublicTools.Constants;
using PublicTools.Resources;
using System.Security.Claims;

namespace OnlineShop.Backoffice.WebApiEndPoint.Controllers.SaleControllers;
[ApiController]
[Route("api/Product")]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpGet("GetAllWithPrivateData")]
    public async Task<IActionResult> GetAllWithPrivateData()
    {
        var getAllOperationResponse = await _productService.GetAllWithPrivateData();
        return getAllOperationResponse.IsSuccessful ? Ok(getAllOperationResponse.ResultModel!.GetResultDtos) : Problem(getAllOperationResponse.ErrorMessage, statusCode: 406);
    }

    [Authorize(Roles = "Seller")]
    [HttpGet("GetSelfProducts")]
    public async Task<IActionResult> GetRangeBySeller()
    {
        var model = new GetProductsRangeBySellerAppDto();
        SetModelRequesterId(model);

        var getOperationResponse = await _productService.GetRangeBySeller(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Policy = PolicyConstants.AdminsOnly)]
    [HttpGet("GetWithPrivateData")]
    public async Task<IActionResult> GetWithPrivateData([FromBody] GetProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);
        var getOperationResponse = await _productService.GetWithPrivateData(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Seller")]
    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromBody] GetProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var getOperationResponse = await _productService.Get(model);
        return getOperationResponse.IsSuccessful ? Ok(getOperationResponse.ResultModel) : Problem(getOperationResponse.ErrorMessage, statusCode: (int)getOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Seller")]
    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] PostProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var postOperationResponse = await _productService.Post(model);
        return postOperationResponse.IsSuccessful ? Ok(postOperationResponse.Message) : Problem(postOperationResponse.ErrorMessage, statusCode: (int)postOperationResponse.HttpStatusCode);
    }

    [Authorize(Roles = "Seller")]
    [HttpPut("Put")]
    public async Task<IActionResult> Put([FromBody] PutProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var putOperationResponse = await _productService.Put(model);
        return putOperationResponse.IsSuccessful ? Ok(putOperationResponse.Message) : Problem(putOperationResponse.ErrorMessage, statusCode: (int)putOperationResponse.HttpStatusCode);
    }

    [Authorize]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductAppDto model)
    {
        if (model is null) return Json(MessageResource.Error_NullInputModel);

        SetModelRequesterId(model);

        var deleteOperationResponse = await _productService.Delete(model);
        return deleteOperationResponse.IsSuccessful ? Ok(deleteOperationResponse.Message) : Problem(deleteOperationResponse.ErrorMessage, statusCode: (int)deleteOperationResponse.HttpStatusCode);
    }

    private void SetModelRequesterId(object model)
    {
        var modelOwnerIdProperty = model.GetType().GetProperties().SingleOrDefault(p => p.IsDefined(typeof(RequesterIdAttribute), false));

        if (modelOwnerIdProperty is null) return;

        modelOwnerIdProperty.SetValue(model, User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)!.Value);
    }
}

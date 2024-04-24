using Microsoft.AspNetCore.Identity;
using OnlineShop.Application.Contracts;
using OnlineShop.Application.Dtos.SaleDtos.OrderHeaderDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Application.Services.SaleServices;
public class OrderService(IOrderRepository orderRepository, IProductRepository productRepository, UserManager<OnlineShopUser> userManager) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly UserManager<OnlineShopUser> _userManager = userManager;

    // Get
    public async Task<IResponse<GetOrderResultAppDto>> Get(GetOrderAppDto model)
    {
        if (model is null) return new Response<GetOrderResultAppDto>(MessageResource.Error_NullInputModel);
        var selectOperationResponse = await _orderRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<GetOrderResultAppDto>(selectOperationResponse.ErrorMessage!);

        var resultDto = new GetOrderResultAppDto
        {
            Id = selectOperationResponse.ResultModel!.Id,
            Code = selectOperationResponse.ResultModel.Code!,
            Buyer = selectOperationResponse.ResultModel.Buyer,
            Seller = selectOperationResponse.ResultModel.Seller,
            CreatedDateGregorian = selectOperationResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectOperationResponse.ResultModel.CreatedDatePersian!,
            IsModified = selectOperationResponse.ResultModel.IsModified,
            ModifyDateGregorian = selectOperationResponse.ResultModel.ModifyDateGregorian,
            ModifyDatePersian = selectOperationResponse.ResultModel.ModifyDatePersian,
        };

        foreach (var orderDetail in selectOperationResponse.ResultModel.OrderDetails)
        {
            var orderDetailDto = new OrderDetailDto
            {
                OrderHeaderId = orderDetail.OrderHeaderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };
            resultDto.OrderDetailDtos.Add(orderDetailDto);
        }

        return new Response<GetOrderResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetAllOrdersResultAppDto>> GetAll()
    {
        var selectOperationResponse = await _orderRepository.SelectAsync();
        if (!selectOperationResponse.IsSuccessful) return new Response<GetAllOrdersResultAppDto>(selectOperationResponse.ErrorMessage!);

        var resultDto = new GetAllOrdersResultAppDto();

        foreach (var order in selectOperationResponse.ResultModel!)
        {
            var getResultDto = new GetOrderResultAppDto
            {
                Id = order.Id,
                Code = order.Code!,
                Buyer = order.Buyer,
                Seller = order.Seller,
                CreatedDateGregorian = order.CreatedDateGregorian,
                CreatedDatePersian = order.CreatedDatePersian!,
                IsModified = order.IsModified,
                ModifyDateGregorian = order.ModifyDateGregorian,
                ModifyDatePersian = order.ModifyDatePersian,
            };

            foreach (var orderDetail in order.OrderDetails)
            {
                var orderDetailTestDto = new OrderDetailDto
                {
                    OrderHeaderId = orderDetail.OrderHeaderId,
                    ProductId = orderDetail.ProductId,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice
                };
                getResultDto.OrderDetailDtos.Add(orderDetailTestDto);
            }
            resultDto.GetResultDtos.Add(getResultDto);
        }

        return new Response<GetAllOrdersResultAppDto>(resultDto);
    }

    // Post
    public async Task<IResponse<object>> Post(PostOrderAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (string.IsNullOrEmpty(model.Code)) return new Response<object>(MessageResource.Error_RequiredField);
        if (string.IsNullOrEmpty(model.SellerId)) return new Response<object>(MessageResource.Error_RequiredField);
        if (string.IsNullOrEmpty(model.BuyerId)) return new Response<object>(MessageResource.Error_RequiredField);

        var seller = await _userManager.FindByIdAsync(model.SellerId);
        if (seller is null) return new Response<object>(MessageResource.Error_SellerNotFound);

        var buyer = await _userManager.FindByIdAsync(model.BuyerId);
        if (buyer is null) return new Response<object>(MessageResource.Error_BuyerNotFound);

        var sellerRoles = await _userManager.GetRolesAsync(seller);
        var buyerRoles = await _userManager.GetRolesAsync(buyer);

        if (!sellerRoles.Contains("Seller")) return new Response<object>(MessageResource.Error_WrongSeller);
        if (!buyerRoles.Contains("Buyer")) return new Response<object>(MessageResource.Error_WrongBuyer);

        if (_orderRepository.SelectByCodeAsync(model.Code).Result.IsSuccessful) return new Response<object>(MessageResource.Error_RepetitiousCode);

        if (model.OrderDetailDtos is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.OrderDetailDtos.Count == 0) return new Response<object>(MessageResource.Error_EmptyOrderDetails);
        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            if (orderDetailDto is null) return new Response<object>(MessageResource.Error_NullOrderDetail);
            if (_productRepository.SelectByIdAsync(orderDetailDto.ProductId).Result.ResultModel is null) return new Response<object>(MessageResource.Error_ProductNotFound);
            if (model.OrderDetailDtos.Select(od => od.ProductId).Where(pid => pid == orderDetailDto.ProductId).ToList().Count > 1) return new Response<object>(MessageResource.Error_DuplicateProductInAnOrder);
            if (orderDetailDto.Quantity <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessQuantity);
            if (orderDetailDto.UnitPrice <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessUnitPrice);
        }

        var newOrder = new OrderHeader
        {
            Id = Guid.NewGuid(),
            Code = model.Code,
            BuyerId = model.BuyerId,
            SellerId = model.SellerId,
        };

        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            var orderDetail = new OrderDetail
            {
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                UnitPrice = orderDetailDto.UnitPrice
            };
            newOrder.OrderDetails.Add(orderDetail);
        }

        var insertOperationResponse = await _orderRepository.InsertAsync(newOrder);

        if (insertOperationResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return insertOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(insertOperationResponse.ErrorMessage!);
    }

    // Put
    public async Task<IResponse<object>> Put(PutOrderAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (string.IsNullOrEmpty(model.Code)) return new Response<object>(MessageResource.Error_RequiredField);
        if (string.IsNullOrEmpty(model.SellerId)) return new Response<object>(MessageResource.Error_RequiredField);
        if (string.IsNullOrEmpty(model.BuyerId)) return new Response<object>(MessageResource.Error_RequiredField);

        var seller = await _userManager.FindByIdAsync(model.SellerId);
        if (seller is null) return new Response<object>(MessageResource.Error_SellerNotFound);
        var buyer = await _userManager.FindByIdAsync(model.BuyerId);
        if (buyer is null) return new Response<object>(MessageResource.Error_BuyerNotFound);

        var sellerRoles = await _userManager.GetRolesAsync(seller);
        var buyerRoles = await _userManager.GetRolesAsync(buyer);

        if (!sellerRoles.Contains("Seller")) return new Response<object>(MessageResource.Error_WrongSeller);
        if (!buyerRoles.Contains("Buyer")) return new Response<object>(MessageResource.Error_WrongBuyer);

        var selectByCodeOperationResponse = await _orderRepository.SelectByCodeAsync(model.Code);

        if (selectByCodeOperationResponse.IsSuccessful && model.Id != selectByCodeOperationResponse.ResultModel!.Id)
            return new Response<object>(MessageResource.Error_RepetitiousCode);

        if (model.OrderDetailDtos is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (model.OrderDetailDtos.Count == 0) return new Response<object>(MessageResource.Error_EmptyOrderDetails);
        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            if (orderDetailDto is null) return new Response<object>(MessageResource.Error_NullOrderDetail);
            if (_productRepository.SelectByIdAsync(orderDetailDto.ProductId).Result.ResultModel is null) return new Response<object>(MessageResource.Error_ProductNotFound);
            if (model.OrderDetailDtos.Select(od => od.ProductId).Where(pid => pid == orderDetailDto.ProductId).ToList().Count > 1) return new Response<object>(MessageResource.Error_DuplicateProductInAnOrder);
            if (orderDetailDto.Quantity <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessQuantity);
            if (orderDetailDto.UnitPrice <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessUnitPrice);
        }

        var selectOperationResponse = await _orderRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(MessageResource.Error_OrderNotFound);

        var updatedOrder = selectOperationResponse.ResultModel;

        updatedOrder!.Code = model.Code;
        updatedOrder!.BuyerId = model.BuyerId;
        updatedOrder!.SellerId = model.SellerId;
        updatedOrder!.IsModified = true;
        updatedOrder!.ModifyDateGregorian = DateTime.Now;
        updatedOrder!.ModifyDatePersian = DateTime.Now.ConvertToPersian();

        await _orderRepository.ClearOrderDetails(updatedOrder);
        updatedOrder!.OrderDetails.Clear();

        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            var orderDetail = new OrderDetail
            {
                OrderHeaderId = model.Id,
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                UnitPrice = orderDetailDto.UnitPrice
            };
            updatedOrder!.OrderDetails.Add(orderDetail);
        }

        var updateOperationResponse = await _orderRepository.UpdateAsync(updatedOrder);

        if (updateOperationResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOperationResponse.ErrorMessage!);
    }

    // Delete
    public async Task<IResponse<object>> Delete(DeleteOrderAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var selectOperationResponse = await _orderRepository.SelectByIdAsync(model.Id);
        if (!selectOperationResponse.IsSuccessful) return new Response<object>(selectOperationResponse.ErrorMessage!);

        var deletedOrder = selectOperationResponse.ResultModel;
        deletedOrder!.IsSoftDeleted = true;
        deletedOrder.SoftDeleteDateGregorian = DateTime.Now;
        deletedOrder.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var updateOperationResponse = await _orderRepository.UpdateAsync(deletedOrder);

        if (updateOperationResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return updateOperationResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOperationResponse.ErrorMessage!);
    }
}
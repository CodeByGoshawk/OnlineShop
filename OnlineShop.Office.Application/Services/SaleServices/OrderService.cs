using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Constants;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Services.SaleServices;
public class OrderService(IOrderRepository orderRepository, IProductRepository productRepository, UserManager<OnlineShopUser> userManager) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly UserManager<OnlineShopUser> _userManager = userManager;

    public async Task<IResponse<GetOrderResultAppDto>> Get(GetOrderAppDto model)
    {
        if (model is null) return new Response<GetOrderResultAppDto>(MessageResource.Error_NullInputModel);
        var selectOrderResponse = await _orderRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectOrderResponse.IsSuccessful || selectOrderResponse.ResultModel!.BuyerId != model.BuyerId)
            return new Response<GetOrderResultAppDto>(MessageResource.Error_UnauthorizedOwner);

        var resultDto = new GetOrderResultAppDto
        {
            Id = selectOrderResponse.ResultModel!.Id,
            BuyerId = selectOrderResponse.ResultModel!.BuyerId,
            Code = selectOrderResponse.ResultModel.Code!,
            CreatedDateGregorian = selectOrderResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectOrderResponse.ResultModel.CreatedDatePersian!,
            IsModified = selectOrderResponse.ResultModel.IsModified,
            ModifyDateGregorian = selectOrderResponse.ResultModel.ModifyDateGregorian,
            ModifyDatePersian = selectOrderResponse.ResultModel.ModifyDatePersian,

            OrderDetails = selectOrderResponse.ResultModel.OrderDetails
            .Select(od => new OrderDetailAppDto
            {
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                SellerId = od.Product!.SellerId
            })
            .ToList()
        };

        return new Response<GetOrderResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeByBuyer(GetOrdersRangeByBuyerAppDto model)
    {
        var buyer = await _userManager.FindByIdAsync(model.BuyerId);
        if (buyer is null || buyer.IsSoftDeleted) return new Response<GetOrdersRangeResultAppDto>(MessageResource.Error_UserNotFound);
        var selectOrdersResponse = await _orderRepository.SelectNonDeletedsByBuyerAsync(model.BuyerId);
        if (!selectOrdersResponse.IsSuccessful) return new Response<GetOrdersRangeResultAppDto>(selectOrdersResponse.ErrorMessage!);

        var resultDto = new GetOrdersRangeResultAppDto();

        selectOrdersResponse.ResultModel!.ToList().ForEach(order =>
        {
            var getResultDto = new GetOrderResultAppDto
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                Code = order.Code!,
                CreatedDateGregorian = order.CreatedDateGregorian,
                CreatedDatePersian = order.CreatedDatePersian!,
                IsModified = order.IsModified,
                ModifyDateGregorian = order.ModifyDateGregorian,
                ModifyDatePersian = order.ModifyDatePersian,
            };

            resultDto.GetResultDtos.Add(getResultDto);
        });

        return new Response<GetOrdersRangeResultAppDto>(resultDto);
    }

    public async Task<IResponse> Post(PostOrderAppDto model)
    {
        #region[Guard]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        var buyer = await _userManager.FindByIdAsync(model.BuyerId!);
        if (buyer is null || buyer.IsSoftDeleted) return new Response(MessageResource.Error_BuyerNotFound);
        if (!await _userManager.IsInRoleAsync(buyer, DatabaseConstants.DefaultRoles.BuyerName)) return new Response(MessageResource.Error_WrongBuyer);
        if (model.OrderDetailDtos.Count == 0) return new Response(MessageResource.Error_EmptyOrderDetails);
        #endregion

        string orderCode;
        while (true)
        {
            orderCode = $"O{CodeMaker.MakeRandom()}";
            if (!(await _orderRepository.SelectByCodeAsync(orderCode)).IsSuccessful) break;
        }

        var newOrder = new OrderHeader
        {
            Id = Guid.NewGuid(),
            Code = orderCode,
            BuyerId = model.BuyerId,
            CreatedDateGregorian = DateTime.Now,
            CreatedDatePersian = DateTime.Now.ConvertToPersian()
        };

        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            if (orderDetailDto is null) return new Response(MessageResource.Error_NullOrderDetail);
            if (model.OrderDetailDtos.Select(od => od.ProductId).Where(pid => pid == orderDetailDto.ProductId).ToList().Count > 1) return new Response(MessageResource.Error_DuplicateProductInAnOrder);
            if (orderDetailDto.Quantity <= 0) return new Response(MessageResource.Error_ZeroOrLessQuantity);

            var selectProductResponse = await _productRepository.SelectByIdAsync(orderDetailDto.ProductId);
            if (!selectProductResponse.IsSuccessful) return new Response(MessageResource.Error_ProductNotFound);
            var orderDetailProduct = selectProductResponse.ResultModel;

            var orderDetail = new OrderDetail
            {
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                UnitPrice = orderDetailProduct!.UnitPrice
            };
            newOrder.OrderDetails.Add(orderDetail);
        }

        var insertOrderResponse = await _orderRepository.InsertAsync(newOrder);

        if (insertOrderResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return insertOrderResponse.IsSuccessful ? new Response(model) : new Response(insertOrderResponse.ErrorMessage!);
    }

    public async Task<IResponse> Put(PutOrderAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);
        var buyer = await _userManager.FindByIdAsync(model.BuyerId!);
        if (buyer is null || buyer.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);
        if (model.OrderDetailDtos.Count == 0) return new Response(MessageResource.Error_EmptyOrderDetails);
        var selectOrderResponse = await _orderRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectOrderResponse.IsSuccessful || selectOrderResponse.ResultModel!.BuyerId != model.BuyerId) return new Response(MessageResource.Error_UnauthorizedOwner);
        #endregion

        var updatedOrder = selectOrderResponse.ResultModel;

        updatedOrder!.IsModified = true;
        updatedOrder!.ModifyDateGregorian = DateTime.Now;
        updatedOrder!.ModifyDatePersian = DateTime.Now.ConvertToPersian();
        updatedOrder!.OrderDetails.Clear();

        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            if (orderDetailDto is null) return new Response(MessageResource.Error_NullOrderDetail);
            if (model.OrderDetailDtos.Select(od => od.ProductId).Where(pid => pid == orderDetailDto.ProductId).ToList().Count > 1) return new Response(MessageResource.Error_DuplicateProductInAnOrder);
            if (orderDetailDto.Quantity <= 0) return new Response(MessageResource.Error_ZeroOrLessQuantity);

            var selectProductResponse = await _productRepository.SelectByIdAsync(orderDetailDto.ProductId);
            if (!selectProductResponse.IsSuccessful) return new Response(MessageResource.Error_ProductNotFound);
            var orderDetailProduct = selectProductResponse.ResultModel;

            var orderDetail = new OrderDetail
            {
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                UnitPrice = orderDetailProduct!.UnitPrice
            };
            updatedOrder!.OrderDetails.Add(orderDetail);
        }

        var updateOrderResponse = await _orderRepository.UpdateAsync(updatedOrder);

        if (updateOrderResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return updateOrderResponse.IsSuccessful ? new Response(model) : new Response(updateOrderResponse.ErrorMessage!);
    }

    public async Task<IResponse> Delete(DeleteOrderAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var selectOrderResponse = await _orderRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectOrderResponse.IsSuccessful || selectOrderResponse.ResultModel!.BuyerId != model.BuyerId)
            return new Response(MessageResource.Error_UnauthorizedOwner);

        var deletedOrder = selectOrderResponse.ResultModel;
        deletedOrder!.IsSoftDeleted = true;
        deletedOrder.SoftDeleteDateGregorian = DateTime.Now;
        deletedOrder.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var updateOrderResponse = await _orderRepository.UpdateAsync(deletedOrder);

        if (updateOrderResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return updateOrderResponse.IsSuccessful ? new Response(model) : new Response(updateOrderResponse.ErrorMessage!);
    }
}
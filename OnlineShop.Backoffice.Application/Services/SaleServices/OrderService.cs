﻿using OnlineShop.Backoffice.Application.Contracts.Sale;
using OnlineShop.Backoffice.Application.Dtos.SaleDtos.OrderDtos;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Services.SaleServices;
public class OrderService(IOrderRepository orderRepository, IProductRepository productRepository) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IResponse<GetOrdersRangeResultAppDto>> GetAllWithPrivateData()
    {
        var selectOrdersResponse = await _orderRepository.SelectAllAsync();
        if (!selectOrdersResponse.IsSuccessful) return new Response<GetOrdersRangeResultAppDto>(selectOrdersResponse.ErrorMessage!);

        var result = new GetOrdersRangeResultAppDto();

        selectOrdersResponse.ResultModel!.ToList().ForEach(order =>
        {
            var getOrderResultDto = new GetOrderResultAppDto
            {
                Id = order.Id,
                Code = order.Code!,
                CreatedDateGregorian = order.CreatedDateGregorian,
                CreatedDatePersian = order.CreatedDatePersian!,
                IsModified = order.IsModified,
                ModifyDateGregorian = order.ModifyDateGregorian,
                ModifyDatePersian = order.ModifyDatePersian,
                IsSoftDeleted = order.IsSoftDeleted,
                SoftDeleteDateGregorian = order.SoftDeleteDateGregorian,
                SoftDeleteDatePersian = order.SoftDeleteDatePersian,

                Buyer = new GetOnlineShopUserResultAppDto
                {
                    Id = order.BuyerId,
                    FirstName = order.Buyer!.FirstName,
                    LastName = order.Buyer.LastName
                }
            };

            result.GetResultDtos.Add(getOrderResultDto);
        });
        return new Response<GetOrdersRangeResultAppDto>(result);
    }

    public async Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeBySeller(GetOrdersRangeBySellerAppDto model)
    {
        if (model is null) return new Response<GetOrdersRangeResultAppDto>(MessageResource.Error_NullInputModel);
        var selectOrdersResponse = await _orderRepository.SelectNonDeletedsBySellerAsync(model.SellerId);
        if (!selectOrdersResponse.IsSuccessful) return new Response<GetOrdersRangeResultAppDto>(selectOrdersResponse.ErrorMessage!);

        var result = new GetOrdersRangeResultAppDto();

        selectOrdersResponse.ResultModel!.ToList().ForEach(order =>
        {
            var getOrderResultDto = new GetOrderResultAppDto
            {
                Id = order.Id,
                Code = order.Code!,
                CreatedDateGregorian = order.CreatedDateGregorian,
                CreatedDatePersian = order.CreatedDatePersian!,
                IsModified = order.IsModified,
                ModifyDateGregorian = order.ModifyDateGregorian,
                ModifyDatePersian = order.ModifyDatePersian,

                Buyer = new GetOnlineShopUserResultAppDto
                {
                    Id = order.BuyerId,
                    FirstName = order.Buyer!.FirstName,
                    LastName = order.Buyer.LastName
                }
            };

            result.GetResultDtos.Add(getOrderResultDto);
        });
        return new Response<GetOrdersRangeResultAppDto>(result);
    }

    public async Task<IResponse<GetOrderResultAppDto>> GetWithPrivateData(GetOrderAppDto model)
    {
        if (model is null) return new Response<GetOrderResultAppDto>(MessageResource.Error_NullInputModel);
        var selectOrderResponse = await _orderRepository.SelectByIdAsync(model.Id);
        if (!selectOrderResponse.IsSuccessful) return new Response<GetOrderResultAppDto>(selectOrderResponse.ErrorMessage!);

        var result = new GetOrderResultAppDto
        {
            Id = selectOrderResponse.ResultModel!.Id,
            Code = selectOrderResponse.ResultModel.Code!,
            CreatedDateGregorian = selectOrderResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectOrderResponse.ResultModel.CreatedDatePersian!,
            IsModified = selectOrderResponse.ResultModel.IsModified,
            ModifyDateGregorian = selectOrderResponse.ResultModel.ModifyDateGregorian,
            ModifyDatePersian = selectOrderResponse.ResultModel.ModifyDatePersian,
            IsSoftDeleted = selectOrderResponse.ResultModel.IsSoftDeleted,
            SoftDeleteDateGregorian = selectOrderResponse.ResultModel.SoftDeleteDateGregorian,
            SoftDeleteDatePersian = selectOrderResponse.ResultModel.SoftDeleteDatePersian,

            Buyer = new GetOnlineShopUserResultAppDto
            {
                Id = selectOrderResponse.ResultModel!.BuyerId,
                FirstName = selectOrderResponse.ResultModel!.Buyer!.FirstName,
                LastName = selectOrderResponse.ResultModel!.Buyer.LastName
            },

            OrderDetailDtos = selectOrderResponse.ResultModel.OrderDetails
            .Select(od => new OrderDetailAppDto
            {
                OrderHeaderId = od.OrderHeaderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice
            })
            .ToList()
        };

        return new Response<GetOrderResultAppDto>(result);
    }

    public async Task<IResponse<GetOrderResultAppDto>> GetWithSellerOrderDetails(GetOrderAppDto model)
    {
        if (model is null) return new Response<GetOrderResultAppDto>(MessageResource.Error_NullInputModel);
        var selectOrdersResponse = await _orderRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectOrdersResponse.IsSuccessful) return new Response<GetOrderResultAppDto>(selectOrdersResponse.ErrorMessage!);

        var result = new GetOrderResultAppDto
        {
            Id = selectOrdersResponse.ResultModel!.Id,
            Code = selectOrdersResponse.ResultModel.Code!,
            CreatedDateGregorian = selectOrdersResponse.ResultModel.CreatedDateGregorian,
            CreatedDatePersian = selectOrdersResponse.ResultModel.CreatedDatePersian!,
            IsModified = selectOrdersResponse.ResultModel.IsModified,
            ModifyDateGregorian = selectOrdersResponse.ResultModel.ModifyDateGregorian,
            ModifyDatePersian = selectOrdersResponse.ResultModel.ModifyDatePersian,

            Buyer = new GetOnlineShopUserResultAppDto
            {
                Id = selectOrdersResponse.ResultModel!.BuyerId,
                FirstName = selectOrdersResponse.ResultModel!.Buyer!.FirstName,
                LastName = selectOrdersResponse.ResultModel!.Buyer.LastName
            },

            OrderDetailDtos = selectOrdersResponse.ResultModel.OrderDetails
            .Where(od => od.Product!.SellerId == model.SellerId)
            .Select(od => new OrderDetailAppDto
            {
                OrderHeaderId = od.OrderHeaderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice
            })
            .ToList()
        };

        return new Response<GetOrderResultAppDto>(result);
    }

    public async Task<IResponse<object>> Put(PutOrderAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        if (model.OrderDetailDtos.Count == 0) return new Response<object>(MessageResource.Error_EmptyOrderDetails);

        var selectOrderResponse = await _orderRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectOrderResponse.IsSuccessful) return new Response<object>(MessageResource.Error_OrderNotFound);

        var updatedOrder = selectOrderResponse.ResultModel;

        updatedOrder!.IsModified = true;
        updatedOrder!.ModifyDateGregorian = DateTime.Now;
        updatedOrder!.ModifyDatePersian = DateTime.Now.ConvertToPersian();
        updatedOrder!.OrderDetails.Clear();

        foreach (var orderDetailDto in model.OrderDetailDtos)
        {
            if (orderDetailDto is null) return new Response<object>(MessageResource.Error_NullOrderDetail);
            if (model.OrderDetailDtos.Select(od => od.ProductId).Where(pid => pid == orderDetailDto.ProductId).ToList().Count > 1) return new Response<object>(MessageResource.Error_DuplicateProductInAnOrder);
            if (orderDetailDto.Quantity <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessQuantity);

            var selectProductResponse = await _productRepository.SelectByIdAsync(orderDetailDto.ProductId);
            if (!selectProductResponse.IsSuccessful) return new Response<object>(MessageResource.Error_ProductNotFound);
            var orderDetailProduct = selectProductResponse.ResultModel;

            var orderDetail = new OrderDetail
            {
                OrderHeaderId = model.Id,
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                UnitPrice = orderDetailProduct!.UnitPrice
            };
            updatedOrder!.OrderDetails.Add(orderDetail);
        }

        var updateOrderResponse = await _orderRepository.UpdateAsync(updatedOrder);

        if (updateOrderResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return updateOrderResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOrderResponse.ErrorMessage!);
    }

    public async Task<IResponse<object>> Delete(DeleteOrderAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var selectOrderResponse = await _orderRepository.SelectNonDeletedByIdAsync(model.Id);
        if (!selectOrderResponse.IsSuccessful) return new Response<object>(selectOrderResponse.ErrorMessage!);

        var deletedOrder = selectOrderResponse.ResultModel;
        deletedOrder!.IsSoftDeleted = true;
        deletedOrder.SoftDeleteDateGregorian = DateTime.Now;
        deletedOrder.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var updateOrderResponse = await _orderRepository.UpdateAsync(deletedOrder);

        if (updateOrderResponse.IsSuccessful) await _orderRepository.SaveAsync();

        return updateOrderResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(updateOrderResponse.ErrorMessage!);
    }
}
using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Office.Application.Contracts.Sale;
using OnlineShop.Office.Application.Dtos.SaleDtos.OrderDtos;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
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
        if (!selectOrderResponse.IsSuccessful) return new Response<GetOrderResultAppDto>(selectOrderResponse.ErrorMessage!);

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

        return new Response<GetOrderResultAppDto>(resultDto);
    }

    public async Task<IResponse<GetOrdersRangeResultAppDto>> GetRangeByBuyer(GetOrdersRangeByBuyerAppDto model)
    {
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

                Buyer = new GetOnlineShopUserResultAppDto
                {
                    Id = order.BuyerId,
                    FirstName = order.Buyer!.FirstName,
                    LastName = order.Buyer.LastName
                }
            };

            resultDto.GetResultDtos.Add(getResultDto);
        });

        return new Response<GetOrdersRangeResultAppDto>(resultDto);
    }

    public async Task<IResponse<object>> Post(PostOrderAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var buyer = await _userManager.FindByIdAsync(model.BuyerId);
        if (buyer is null || buyer.IsSoftDeleted) return new Response<object>(MessageResource.Error_BuyerNotFound);
        if (!_userManager.IsInRoleAsync(buyer, DatabaseConstants.DefaultRoles.BuyerName).Result) return new Response<object>(MessageResource.Error_WrongBuyer);

        if (model.OrderDetailDtos.Count == 0) return new Response<object>(MessageResource.Error_EmptyOrderDetails);

        string orderCode;
        while (true)
        {
            orderCode = $"O{CodeMaker.MakeRandom()}";
            if (!_orderRepository.SelectByCodeAsync(orderCode).Result.IsSuccessful) break;
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
            if (orderDetailDto is null) return new Response<object>(MessageResource.Error_NullOrderDetail);
            if (model.OrderDetailDtos.Select(od => od.ProductId).Where(pid => pid == orderDetailDto.ProductId).ToList().Count > 1) return new Response<object>(MessageResource.Error_DuplicateProductInAnOrder);
            if (orderDetailDto.Quantity <= 0) return new Response<object>(MessageResource.Error_ZeroOrLessQuantity);

            var selectProductResponse = await _productRepository.SelectByIdAsync(orderDetailDto.ProductId);
            if (!selectProductResponse.IsSuccessful) return new Response<object>(MessageResource.Error_ProductNotFound);
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

        return insertOrderResponse.IsSuccessful ? new Response<object>(model) : new Response<object>(insertOrderResponse.ErrorMessage!);
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

    public async Task<IResponse<GetOrderResultAppDto>> Authorize(Guid id)
    {
        var selectOrderResponse = await _orderRepository.SelectByIdAsync(id);
        if (!selectOrderResponse.IsSuccessful) return new Response<GetOrderResultAppDto>(selectOrderResponse.ErrorMessage!);

        var result = new GetOrderResultAppDto
        {
            BuyerId = selectOrderResponse.ResultModel!.BuyerId,
        };
        return new Response<GetOrderResultAppDto>(result);
    }
}
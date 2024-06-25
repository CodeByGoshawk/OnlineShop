using PublicTools.Attributes;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductsRangeBySellerAppDto
{
    [RequesterId]
    public string SellerId { get; set; }
}

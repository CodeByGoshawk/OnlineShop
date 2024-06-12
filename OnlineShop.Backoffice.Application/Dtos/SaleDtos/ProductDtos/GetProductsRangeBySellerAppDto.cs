using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class GetProductsRangeBySellerAppDto
{
    [OwnerId]
    [Required(ErrorMessage = "Seller Id is required")]
    public string SellerId { get; set; }
}

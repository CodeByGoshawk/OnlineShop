using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductDtos;
public class PostProductAppDto
{
    [Required(ErrorMessage = "Category Id is required")]
    public int ProductCategoryId { get; set; }

    [OwnerId]
    [Required(ErrorMessage = "Seller Id is required")]
    public string SellerId { get; set; }


    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Unit Price is required")]
    public decimal UnitPrice { get; set; }
}

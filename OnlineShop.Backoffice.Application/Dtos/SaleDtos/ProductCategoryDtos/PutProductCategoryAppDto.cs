namespace OnlineShop.Backoffice.Application.Dtos.SaleDtos.ProductCategoryDtos;
public class PutProductCategoryAppDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    public string Title { get; set; }
}

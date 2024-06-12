namespace OnlineShop.Application.Dtos.SaleDtos.OrderDtos;

public class OrderDetailDto
{
    public Guid? OrderHeaderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
}

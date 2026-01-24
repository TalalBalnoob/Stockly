namespace Stockly.Application.DTOs;

public class NewOrderItemDto {
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public decimal? Price { get; set; }
}
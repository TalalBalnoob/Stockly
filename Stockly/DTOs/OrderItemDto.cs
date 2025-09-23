namespace Stockly.DTOs;

public class OrderItemDto {
	public int? id { get; set; } // provided when update 
	public int? orderId { get; set; }
	public int productId { get; set; }
	public int Quantity { get; set; }
	public decimal? UnitPrice { get; set; }
}

namespace Stockly.DTOs;

public class CreateOrderDto {
	public int? Id { get; set; }
	public string? customer_Name { get; set; }
	public string? Customer_Contact { get; set; }
	public List<OrderItemDto>? Items { get; set; }
	public string? PaymentMethod { get; set; }
	public string? PaymentNotes { get; set; }
	public string? Status { get; set; }
}

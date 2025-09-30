namespace Stockly.DTOs;

public class CreateOrderDto {
	public int? Id { get; set; }
	public string? Customer_name { get; set; }
	public string? Customer_contact { get; set; }
	public List<OrderItemDto>? Items { get; set; }
	public string? Payment_method { get; set; }
	public string? Payment_notes { get; set; }
	public string? Status { get; set; }
}

using Stockly.Domain.Enums;

namespace Stockly.Application.DTOs.Orders;

public class CreateOrderRequest {

	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public Order_status Status { get; set; } = Order_status.Pending;
	public Payment_status PaymentStatus { get; set; } = Payment_status.Pending;
	public decimal Total { get; set; } = 0;
	public string? PaymentMethod { get; set; }
	public string? PaymentReference { get; set; }
	public string? ShippingAddress { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public List<CreateOrderItemRequest> OrderItems { get; set; } = new();

}

public class CreateOrderItemRequest {
	public Guid ProductId { get; set; }
	public int Quantity { get; set; } = 1;
	public decimal CustomPrice { get; set; } = 0;
}

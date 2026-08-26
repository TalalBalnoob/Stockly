using Stockly.Domain.Enums;

namespace Stockly.Application.DTOs.Orders;

public class UpdateOrderRequest {

	public Guid Id { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public Order_status? Status { get; set; }
	public Payment_status? PaymentStatus { get; set; }
	public decimal? Total { get; set; }
	public string? PaymentMethod { get; set; }
	public string? PaymentReference { get; set; }
	public string? ShippingAddress { get; set; }

	public DateTime? CreatedAt { get; set; }

	public List<CreateOrderItemRequest>? OrderItems { get; set; }

}

public class UpdateOrderItemRequest {
	public Guid ProductId { get; set; }
	public int? Quantity { get; set; } = 1;
	public decimal? CustomPrice { get; set; } = 0;
}

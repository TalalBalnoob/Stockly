using Stockly.Domain.Enums;

namespace Stockly.Application.DTOs.Orders;

public class OrderResponseDto {
	public Guid Id { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public Order_status Status { get; set; }
	public Payment_status PaymentStatus { get; set; }
	public decimal Total { get; set; }
	public string? PaymentMethod { get; set; }
	public string? PaymentReference { get; set; }
	public string? ShippingAddress { get; set; }
	public DateTime CreatedAt { get; set; }
	public ICollection<OrderItemResponseDto> OrderItems { get; set; } = [];
}

public class OrderItemResponseDto {
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public decimal Price { get; set; }
}

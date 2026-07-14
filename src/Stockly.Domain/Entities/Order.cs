using System.ComponentModel.DataAnnotations;

using Stockly.Domain.Enums;

namespace Stockly.Domain.Entities;

public class Order {
	[Key] public Guid Id { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public Order_status Status { get; set; } = Order_status.Pending;
	public Payment_status PaymentStatus { get; set; } = Payment_status.Pending;
	[Range(0, double.MaxValue, ErrorMessage = "Price can't be negative, we need to eat!")]
	public decimal Total { get; set; } = 0;
	public string? PaymentMethod { get; set; }
	public string? PaymentReference { get; set; }
	public string? ShippingAddress { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


	public ICollection<OrderItem> OrderItems { get; set; } = [];
}

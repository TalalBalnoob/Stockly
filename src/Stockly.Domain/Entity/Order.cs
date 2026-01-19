using System.ComponentModel.DataAnnotations;

namespace Stockly.Domain.Entity;

public class Order {
	[Key] public Guid Id { get; set; }
	public string Status { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Note { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public string? CustomerAddress { get; set; }
	public string? PaymentDetails { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public OrderItem[] OrderItems { get; set; } = Array.Empty<OrderItem>();
}
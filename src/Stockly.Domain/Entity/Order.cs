using System.ComponentModel.DataAnnotations;
using Stockly.Domain.Enums;

namespace Stockly.Domain.Entity;

public class Order {
	[Key] public Guid Id { get; set; }
	public OrderStatus Status { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Note { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public string? CustomerAddress { get; set; }
	public PaymentStatus PaymentStatus { get; set; }
	public string? PaymentDetails { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public OrderItem[] OrderItems { get; set; } = Array.Empty<OrderItem>();
}
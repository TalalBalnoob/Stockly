using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockly.Domain.Entity;

public class OrderItem {
	[Key] public Guid Id { get; set; }
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public decimal Price { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	[ForeignKey(nameof(OrderId))] public Order Order { get; set; }
}
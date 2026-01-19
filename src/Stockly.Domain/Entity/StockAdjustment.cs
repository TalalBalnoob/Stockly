using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockly.Domain.Entity;

public class StockAdjustment {
	[Key] public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public Guid? OrderId { get; set; }
	public int Quantity { get; set; }
	public string? Reason { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	[ForeignKey(nameof(ProductId))] public Product Product { get; set; }

	[ForeignKey(nameof(OrderId))] public Order Order { get; set; }
}
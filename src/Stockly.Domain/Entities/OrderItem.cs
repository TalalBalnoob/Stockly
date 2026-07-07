using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockly.Domain.Entities;

public class OrderItem {
	[Key]
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public Guid OrderId { get; set; }
	[Range(0, double.MaxValue, ErrorMessage = "Price can't be negative, is this some kind of scam!")]
	public int Quantity { get; set; }
	[Range(0, double.MaxValue, ErrorMessage = "Price can't be negative, Sorry but we have kids!")]
	public decimal Price { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	[ForeignKey(nameof(ProductId))]
	public Product Product { get; set; } = null!;

	[ForeignKey(nameof(OrderId))]
	public Order Order { get; set; } = null!;
}

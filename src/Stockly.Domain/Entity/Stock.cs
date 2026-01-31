using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockly.Domain.Entity;

public class Stock {
	[Key] public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public string? StorageNote { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
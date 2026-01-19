using System.ComponentModel.DataAnnotations;

namespace Stockly.Domain.Entity;

public class Product {
	[Key] public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public string? ImageUrl { get; set; }
	public bool IsActive { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
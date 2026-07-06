using System.ComponentModel.DataAnnotations;

namespace Stockly.Domain.Entities;

public class Product {
	[Key]
	public Guid Id { get; set; }

	[Required]
	[StringLength(150, ErrorMessage = "Name is too long, but you know what is even longer?")]
	public string Name { get; set; }

	[Required]
	[Range(0, double.MaxValue, ErrorMessage = "Price can't be negative, lets face it no one is that generous")]
	public decimal Price { get; set; }

	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Quantity can't be negative, are you storing in a black hole??")]
	public int Quantity { get; set; }

	public string Description { get; set; } = string.Empty;
	public string StorageNote { get; set; } = string.Empty;
	public bool IsAvailable { get; set; }

	public DateTime CreatedAt { get; set; }
}

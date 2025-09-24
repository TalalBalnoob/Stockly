using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class Product {
	[Key] public int Id { get; set; }
	[Required] public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Storage_Note { get; set; } = string.Empty;
	[Required] public decimal Price { get; set; }
	[Required] public int Quantity { get; set; }
	public bool IsActive { get; set; } = true;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

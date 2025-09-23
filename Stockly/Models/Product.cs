using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class Product {
	[Key] public int Id { get; set; }
	[Required] public string Name { get; set; }
	public string? Description { get; set; }
	[Required] public decimal Price { get; set; }
	[Required] public int Quantity { get; set; }
	public string? ImageUrl { get; set; }
	public bool? IsActive { get; set; }

	[Column(TypeName = "timestamp")]
	public DateTime CreatedAt { get; set; }

	public Product() {
		CreatedAt = DateTime.UtcNow;
	}
}

using System.ComponentModel.DataAnnotations;

namespace Stockly.Application.DTOs;

public class NewProductDto {
	public string Name { get; set; }
	public string? Description { get; set; } = string.Empty;

	[Range(0, int.MaxValue, ErrorMessage = "Can't have negative Price")]
	public decimal Price { get; set; }

	public bool? IsActive { get; set; } = true;

	[Range(0, int.MaxValue, ErrorMessage = "Can't have negative quantity")]
	public int InialQuantity { get; set; }
}
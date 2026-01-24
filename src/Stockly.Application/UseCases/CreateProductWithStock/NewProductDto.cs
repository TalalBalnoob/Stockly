namespace Stockly.Application.DTOs;

public class NewProductDto {
	public string Name { get; set; }
	public string? Description { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public bool? IsActive { get; set; } = true;
	public int InialQuantity { get; set; }
}
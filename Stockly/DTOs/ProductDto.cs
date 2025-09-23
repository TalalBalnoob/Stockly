namespace Stockly.DTOs;

public class ProductDto {
	public string? Name { get; set; }
	public string? Description { get; set; }
	public decimal? Price { get; set; }
	public int? Quantity { get; set; }
	public string? ImageUrl { get; set; }
	public bool? IsActive { get; set; }
}

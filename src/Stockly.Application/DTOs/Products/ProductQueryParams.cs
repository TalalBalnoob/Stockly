// Application/DTOs/Products/ProductQueryParams.cs
namespace Stockly.Application.DTOs.Products;

public class ProductQueryParams {
	public string? Search { get; set; }
	public bool? IsAvailable { get; set; }
	public int? MinQuantity { get; set; }
	public int? MaxQuantity { get; set; }
}

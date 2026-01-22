namespace Stockly.Application.DTOs;

public class NewStockDto {
	public Guid ProductId { get; set; }
	public int InialQuantity { get; set; }
	public string? StorageNote { get; set; }
}
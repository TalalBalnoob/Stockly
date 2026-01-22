namespace Stockly.Application.DTOs;

public class UpdateStockDto {
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public int InialQuantity { get; set; }
	public string? StorageNote { get; set; }
}
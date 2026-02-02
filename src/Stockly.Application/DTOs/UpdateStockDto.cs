namespace Stockly.Application.DTOs;

public class UpdateStockDto {
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
	public string? StorageNote { get; set; }
	public string? Reason { get; set; }
}
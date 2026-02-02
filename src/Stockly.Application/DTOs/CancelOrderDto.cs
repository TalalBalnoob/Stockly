namespace Stockly.API.ApiDtos.OrderDtos;

public class CancelOrderDto {
	public string Reason { get; set; } = string.Empty;
	public bool Restock { get; set; } = false;
}
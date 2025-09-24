using System;

namespace Stockly.DTOs;

public class StockDto {
	public int? Id { get; set; }
	public int? Change { get; set; }
	public string? Reason { get; set; }
	public int? Product_Id { get; set; }
	public DateTime? CreatedAt { get; set; }
	public int? Related_Order_Id { get; set; }
}

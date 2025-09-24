using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class StockAdjustment {
	[Key] public int Id { get; set; }
	[Required] public int Product_Id { get; set; }
	public int? Related_Order_Id { get; set; } // when the stock log is related to an order
	[Required] public int Change { get; set; }
	public string Reason { get; set; } = string.Empty;

	[ForeignKey("Product_Id")]
	public Product? Product { get; set; }

	[ForeignKey("Related_Order_Id")]
	public Order? Related_Order { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class OrderItem {
	[Key]
	public int Id { get; set; }
	[Required] public int OrderId { get; set; }
	[Required] public int ProductId { get; set; }
	[Required] public int Quantity { get; set; }
	[Required] public decimal Price { get; set; }
	[Required] public decimal Subtotle { get; set; }


	[ForeignKey("OrderId")] public Order Order { get; set; }
	[ForeignKey("ProductId")] public Product Product { get; set; }
}

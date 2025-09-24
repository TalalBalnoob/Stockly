using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Stockly.Statics;

namespace Stockly.Models;

[ValidateNever]
public class Order {
	[Key] public int Id { get; set; }
	public string Customer_Name { get; set; } = string.Empty;
	public string Customer_Contact { get; set; } = string.Empty;
	[Required] public string Status { get; set; } = OrderStatuses.Payment_Pending;
	[Required] public decimal Total_amount { get; set; }
	public List<OrderItem> Items { get; set; } = new List<OrderItem>();

	public string PaymentMethod { get; set; } = PaymentMethods.None; // e.g. "Cash", "Mada"

	public string PaymentNotes { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

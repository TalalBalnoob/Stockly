using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockly.Domain.Entities;

public class StockAdjustment {
	[Key]
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public Guid? RelatedOrderId { get; set; }
	public int Change { get; set; }
	public string Reason { get; set; } = string.Empty;


	public DateTime AdjustmentDate { get; set; } = DateTime.UtcNow;

	[ForeignKey(nameof(ProductId))]
	public Product Product { get; set; } = null!;
}

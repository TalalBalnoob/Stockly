using System.ComponentModel.DataAnnotations;

namespace Stockly.Application.DTOs.StockAdjustments;

public class CreateStockAdjustmentDto
{
    [Required]
    public Guid ProductId { get; set; }

    public Guid? RelatedOrderId { get; set; }

    [Required]
    public int Change { get; set; }

    [Required]
    [StringLength(250)]
    public string Reason { get; set; } = string.Empty;
}

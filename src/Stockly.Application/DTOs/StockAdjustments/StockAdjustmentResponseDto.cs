namespace Stockly.Application.DTOs.StockAdjustments;

public class StockAdjustmentResponseDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public Guid? RelatedOrderId { get; set; }
    public int Change { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime AdjustmentDate { get; set; }
}

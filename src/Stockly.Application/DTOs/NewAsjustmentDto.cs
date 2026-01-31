using System.ComponentModel.DataAnnotations;

namespace Stockly.Application.DTOs;

public class NewAsjustmentDto
{
    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; }

    public Guid? OrderId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
    public string? Reason { get; set; }
}
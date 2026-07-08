using System.ComponentModel.DataAnnotations;

namespace Stockly.Application.DTOs.Products;

public class UpdateProductDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public string Description { get; set; } = string.Empty;
    public string StorageNote { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}

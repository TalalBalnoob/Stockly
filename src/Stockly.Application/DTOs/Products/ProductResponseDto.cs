namespace Stockly.Application.DTOs.Products;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public string StorageNote { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
}

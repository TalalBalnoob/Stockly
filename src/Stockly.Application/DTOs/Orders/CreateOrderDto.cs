using System.ComponentModel.DataAnnotations;
using Stockly.Domain.Enums;

namespace Stockly.Application.DTOs.Orders;

public class CreateOrderDto
{
    [StringLength(100)]
    public string? CustomerName { get; set; }

    [Phone]
    public string? CustomerContect { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(250)]
    public string? ShippingAddress { get; set; }

    [Required]
    public ICollection<CreateOrderItemDto> OrderItems { get; set; } = [];
}

public class CreateOrderItemDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

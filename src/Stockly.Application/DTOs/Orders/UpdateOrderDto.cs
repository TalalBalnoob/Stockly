using System.ComponentModel.DataAnnotations;
using Stockly.Domain.Enums;

namespace Stockly.Application.DTOs.Orders;

public class UpdateOrderDto
{
    [StringLength(100)]
    public string? CustomerName { get; set; }

    [Phone]
    public string? CustomerContect { get; set; }

    public Order_status? Status { get; set; }

    public Payment_status? PaymentStatus { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(100)]
    public string? PaymentReference { get; set; }

    [StringLength(250)]
    public string? ShippingAddress { get; set; }
}

namespace Stockly.DTOs;

public class OrderDto{
    public int?  Id { get; set; }
    public string? customerName { get; set; }
    public string? CustomerContact { get; set; }
    public List<OrderItemDto>? Items { get; set; }
    // public string? PaymentMethod { get; set; }
    // public string? PaymentNotes { get; set; }
    public string? Status { get; set; }
}
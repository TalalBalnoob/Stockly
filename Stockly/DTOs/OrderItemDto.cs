namespace Stockly.DTOs;

public class OrderItemDto{
    public int product_id { get; set; }
    public int Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
}
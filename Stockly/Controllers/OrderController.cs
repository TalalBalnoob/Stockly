using Microsoft.AspNetCore.Mvc;
using Stockly.DTOs;
using Stockly.Models;
using Stockly.Statics;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController: Controller{
    private readonly AppDbContext _db;

    public OrderController(AppDbContext db){
        _db = db;
    }

    [HttpPost]
    public ActionResult Create(OrderDto orderDto){
        Order order = new Order{
            Customer_Name = orderDto.customerName,
            Customer_Contact = orderDto.CustomerContact,
            Status = OrderStatuses.Approved,
            PaymentMethod = orderDto.PaymentMethod ?? "",
            PaymentNotes = orderDto.PaymentNotes ?? "",
        };
        
        List<OrderItem> orderItems = new List<OrderItem>();

        foreach (OrderItemDto item in orderDto.Items){
            var product = _db.Products.Find(item.product_id);
            if(product == null) return NotFound("Product not found");
            
            orderItems.Add(new OrderItem{
                ProductId = item.product_id,
                Price = item.UnitPrice ?? product.Price,
                Quantity = item.Quantity,
                Order = order,
                Subtotle = item.Quantity * (item.UnitPrice ?? product.Price),
            });
            
            order.Totel_amount = (item.UnitPrice ?? product.Price) * item.Quantity;

            var ItemStock = new StockAdjustment{
                Change = -item.Quantity,
                Reason = "order",
                Product_Id = item.product_id,
            };
            _db.StockAdjustment.Add(ItemStock);
            
            
            product.Quantity -= item.Quantity;
            _db.Products.Update(product);
        }
        
        
        _db.OrderItems.AddRange(orderItems);
        _db.Orders.Add(order);
        _db.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult CancelOrder(int id){
        var order = _db.Orders.Find(id);
        if(order == null) return NotFound("Order not found");

        order.Status = OrderStatuses.Cancelled;
        
        List<OrderItem> orderItems = _db.OrderItems.Where(x => x.OrderId == id).ToList();
        foreach (var item in orderItems){
            var product = _db.Products.Find(item.ProductId);
            var ItemStock = new StockAdjustment{
                Change = item.Quantity,
                Reason = "Cancelled order",
                Product_Id = item.ProductId,
            };
            
            product.Quantity += item.Quantity;
            _db.StockAdjustment.Add(ItemStock);
        }

        _db.SaveChanges();
        return NoContent();
    }
    
}
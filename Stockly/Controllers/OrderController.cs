using Microsoft.AspNetCore.Mvc;
using Stockly.DTOs;
using Stockly.Models;
using Stockly.Statics;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller{
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
            Totel_amount = 0
            // PaymentMethod = orderDto.PaymentMethod ?? "",
            // PaymentNotes = orderDto.PaymentNotes ?? "",
        };

        List<OrderItem> orderItems = new List<OrderItem>();

        foreach (OrderItemDto item in orderDto.Items){
            var product = _db.Products.Find(item.productId);
            if (product == null) return NotFound("Product not found");

            var newOrderItem = new OrderItem{
                ProductId = item.productId,
                Price = item.UnitPrice ?? product.Price,
                Quantity = item.Quantity,
                Order = order,
                Subtotle = item.Quantity * (item.UnitPrice ?? product.Price),
            };
            orderItems.Add(newOrderItem);

            order.Totel_amount += newOrderItem.Subtotle;

            var ItemStock = new StockAdjustment{
                Change = -item.Quantity,
                Reason = "order",
                Product_Id = item.productId,
                Related_Order_Id = item.orderId
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

    [HttpPut("{id}")]
    public ActionResult Update(int id, OrderDto orderDto){
        var orderFromDb = _db.Orders.Find(id);
        if (orderFromDb == null) return NotFound("Order not found");

        var orderItemsList = _db.OrderItems.Where(x => x.OrderId == id).ToList();
        foreach (var orderItem in orderItemsList){
            var product = _db.Products.Find(orderItem.ProductId);
            product.Quantity += orderItem.Quantity;
        }

        orderFromDb.Customer_Name = orderDto.customerName;
        orderFromDb.Customer_Contact = orderDto.CustomerContact;
        orderFromDb.Status = orderDto.Status ?? orderFromDb.Status;

        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (var item in orderDto.Items){
            var product = _db.Products.Find(item.productId);
            orderItems.Add(new OrderItem{
                ProductId = item.productId,
                Price = item.UnitPrice ?? product.Price,
                Quantity = item.Quantity,
                Order = orderFromDb,
                Subtotle = item.Quantity * (item.UnitPrice ?? product.Price),
            });
            product.Quantity -= item.Quantity;
            if (product.Quantity < 0){
                return BadRequest("Insufficient quantity");
            }
        }
        
        foreach (var item in orderItems){
            var existing = orderItemsList.FirstOrDefault(x => x.ProductId == item.ProductId);
            
            if (existing == null){
                var adjustment = new StockAdjustment{
                    Product_Id = item.ProductId,
                    Change = -item.Quantity,
                    Reason = "Order Updated",
                    Related_Order_Id = item.OrderId
                };
                _db.StockAdjustment.Add(adjustment);
            }
            else if (existing.Quantity != item.Quantity){
                int diff = item.Quantity - existing.Quantity;
                var adjustment = new StockAdjustment{
                    Product_Id = item.ProductId,
                    Change = -diff,
                    Reason = "Order Updated",
                    Related_Order_Id = item.OrderId
                };
                _db.StockAdjustment.Add(adjustment);
            }
        }

        foreach (var oldItem in orderItemsList){
            if (!orderItems.Any(x => x.ProductId == oldItem.ProductId)){
                var adjustment = new StockAdjustment{
                    Product_Id = oldItem.ProductId,
                    Change = -oldItem.Quantity,
                    Reason = "Order Updated",
                    Related_Order_Id = oldItem.OrderId
                };
                _db.StockAdjustment.Add(adjustment);
            }
        }
        
        orderFromDb.Totel_amount = orderItems.Where(x => x.Quantity > 0).Sum(x => x.Subtotle);
        _db.OrderItems.RemoveRange(orderItemsList);
        _db.OrderItems.AddRange(orderItems);
        _db.SaveChanges();
        
        return Ok();
    }

    [HttpPut("ship/{id}")]
    public ActionResult Ship(int id){
        var orderFromDb = _db.Orders.Find(id);
        if (orderFromDb == null) return NotFound("Order not found");
        
        orderFromDb.Status = OrderStatuses.Shipped;
        _db.SaveChanges();
        return Ok("order shipped");
    }
    
    [HttpPut("deliver/{id}")]
    public ActionResult Deliver(int id){
        var orderFromDb = _db.Orders.Find(id);
        if (orderFromDb == null) return NotFound("Order not found");
        
        orderFromDb.Status = OrderStatuses.Delivered;
        _db.SaveChanges();
        return Ok("order Delivered");
    }

    [HttpPut("return/{id}")]
    public ActionResult Return(int id){
        var orderFromDb = _db.Orders.Find(id);
        if (orderFromDb == null) return NotFound("Order not found");
        
        orderFromDb.Status = OrderStatuses.Returned;
        
        var orderItems = _db.OrderItems.Where(x => x.OrderId == id).ToList();
        foreach (var orderItem in orderItems){
            var product = _db.Products.Find(orderItem.ProductId);
            product.Quantity += orderItem.Quantity;

            var stockAdjustment = new StockAdjustment{
                Change = orderItem.Quantity,
                Reason = "Returned order",
                Product_Id = orderItem.ProductId,
                Related_Order_Id = orderItem.OrderId,
            };
            _db.StockAdjustment.Add(stockAdjustment);
        }
        
        _db.SaveChanges();
        return Ok("order shipped");
    }
    
    [HttpDelete("{id}")]
    public ActionResult CancelOrder(int id){
        var order = _db.Orders.Find(id);
        if (order == null) return NotFound("Order not found");

        order.Status = OrderStatuses.Cancelled;

        List<OrderItem> orderItems = _db.OrderItems.Where(x => x.OrderId == id).ToList();
        foreach (var item in orderItems){
            var product = _db.Products.Find(item.ProductId);
            var ItemStock = new StockAdjustment{
                Change = item.Quantity,
                Reason = "Cancelled order",
                Product_Id = item.ProductId,
                Related_Order_Id = item.OrderId,
            };

            product.Quantity += item.Quantity;
            _db.StockAdjustment.Add(ItemStock);
        }

        _db.SaveChanges();
        return NoContent();
    }
}
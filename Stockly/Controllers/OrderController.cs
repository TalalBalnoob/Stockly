using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Stockly.DTOs;
using Stockly.Models;
using Stockly.Statics;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(AppDbContext _db) : Controller {

	[HttpGet]
	public ActionResult<IEnumerable<CreateOrderDto>> Get() {
		var orders = _db.Orders.Include(u => u.Items).Select(o => new OrderDto {
			Id = o.Id,
			Customer_name = o.Customer_name,
			Customer_contact = o.Customer_contact,
			Payment_method = o.Payment_method,
			Payment_notes = o.Payment_notes,
			Status = o.Status,
			Order_total = o.Total_amount,
			Items = o.Items.Select(i => new OrderItemDto {
				orderId = i.OrderId,
				productId = i.ProductId,
				Quantity = i.Quantity,
				UnitPrice = i.Price,
			}).ToList()
		}).ToList();
		return Ok(orders);
	}

	[HttpGet("{id}")]
	public ActionResult<CreateOrderDto> Get(int id) {
		var orderFromDb = _db.Orders
			.Include(u => u.Items).Select(o => new OrderDto {
				Id = o.Id,
				Customer_name = o.Customer_name,
				Customer_contact = o.Customer_contact,
				Payment_method = o.Payment_method,
				Payment_notes = o.Payment_notes,
				Status = o.Status,
				Order_total = o.Total_amount,
				Items = o.Items.Select(i => new OrderItemDto {
					id = i.Id,
					orderId = i.OrderId,
					productId = i.ProductId,
					Quantity = i.Quantity,
					UnitPrice = i.Price,
				}).ToList()
			})
			.FirstOrDefault(o => o.Id == id);
		if (orderFromDb == null) return NotFound("Order not found");

		return Ok(orderFromDb);
	}

	[HttpPost]
	public ActionResult Create(CreateOrderDto orderDto) {
		Order order = new Order {
			Id = 0,
			Customer_name = orderDto.Customer_name ?? "",
			Customer_contact = orderDto.Customer_contact ?? "",
			Status = orderDto.Status ?? OrderStatuses.Payment_Pending,
			Total_amount = 0,
			Payment_method = orderDto.Payment_method ?? PaymentMethods.None,
			Payment_notes = orderDto.Payment_notes ?? "",
			CreatedAt = DateTime.Now,
		};

		List<OrderItem> orderItems = new List<OrderItem>();

		foreach (OrderItemDto item in orderDto.Items) {
			var product = _db.Products.Find(item.productId);
			if (product == null) return NotFound("Product not found");

			var newOrderItem = new OrderItem {
				ProductId = item.productId,
				Price = item.UnitPrice ?? product.Price,
				Quantity = item.Quantity,
				Order = order,
				Total = item.Quantity * (item.UnitPrice ?? product.Price),
			};
			orderItems.Add(newOrderItem);

			order.Total_amount += newOrderItem.Total;

			var ItemStock = new StockAdjustment {
				Change = -item.Quantity,
				Reason = "order",
				Product_Id = item.productId,
				Related_Order = order
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
	public ActionResult Update(int id, CreateOrderDto orderDto) {
		var orderFromDb = _db.Orders.Find(id);
		if (orderFromDb == null) return NotFound("Order not found");

		var orderItemsList = _db.OrderItems.Where(x => x.OrderId == id).ToList();
		foreach (var orderItem in orderItemsList) {
			var product = _db.Products.Find(orderItem.ProductId);
			if (product == null) return NotFound("Product not found");
			product.Quantity += orderItem.Quantity;
		}

		orderFromDb.Customer_name = orderDto.Customer_name ?? "";
		orderFromDb.Customer_contact = orderDto.Customer_contact ?? "";
		orderFromDb.Status = orderDto.Status ?? orderFromDb.Status;
		orderFromDb.Payment_method = orderDto.Payment_method ?? orderFromDb.Payment_method;
		orderFromDb.Payment_notes = orderDto.Payment_notes ?? orderFromDb.Payment_notes;

		List<OrderItem> orderItems = new List<OrderItem>();
		foreach (var item in orderDto.Items) {
			var product = _db.Products.Find(item.productId);
			if (product == null) return NotFound("Product not found");

			orderItems.Add(new OrderItem {
				ProductId = item.productId,
				Price = item.UnitPrice ?? product.Price,
				Quantity = item.Quantity,
				Order = orderFromDb,
				Total = item.Quantity * (item.UnitPrice ?? product.Price),
			});
			product.Quantity -= item.Quantity;

			if (product.Quantity < 0) {
				return BadRequest("Insufficient quantity");
			}
		}

		foreach (var item in orderItems) {
			var existing = orderItemsList.FirstOrDefault(x => x.ProductId == item.ProductId);

			if (existing == null) {
				var adjustment = new StockAdjustment {
					Product_Id = item.ProductId,
					Change = -item.Quantity,
					Reason = "Order Updated",
					Related_Order_Id = item.OrderId
				};
				_db.StockAdjustment.Add(adjustment);
			}
			else if (existing.Quantity != item.Quantity) {
				int diff = item.Quantity - existing.Quantity;
				var adjustment = new StockAdjustment {
					Product_Id = item.ProductId,
					Change = -diff,
					Reason = "Order Updated",
					Related_Order_Id = item.OrderId
				};
				_db.StockAdjustment.Add(adjustment);
			}
		}

		foreach (var oldItem in orderItemsList) {
			if (!orderItems.Any(x => x.ProductId == oldItem.ProductId)) {
				var adjustment = new StockAdjustment {
					Product_Id = oldItem.ProductId,
					Change = -oldItem.Quantity,
					Reason = "Order Updated",
					Related_Order_Id = oldItem.OrderId
				};
				_db.StockAdjustment.Add(adjustment);
			}
		}

		orderFromDb.Total_amount = orderItems.Where(x => x.Quantity > 0).Sum(x => x.Total);
		_db.OrderItems.RemoveRange(orderItemsList);
		_db.OrderItems.AddRange(orderItems);
		_db.SaveChanges();

		return Ok();
	}

	[HttpPut("approve/{id}")]
	public ActionResult Approve(int id) {
		var orderFromDb = _db.Orders.Find(id);
		if (orderFromDb == null) return NotFound("Order not found");

		if (orderFromDb.Status == OrderStatuses.Payment_Pending) {
			orderFromDb.Status = OrderStatuses.Approved;
			_db.SaveChanges();
			return Ok("order Approved");
		}
		else {
			return BadRequest("order not Approved");
		}
	}

	[HttpPut("ship/{id}")]
	public ActionResult Ship(int id) {
		var orderFromDb = _db.Orders.Find(id);
		if (orderFromDb == null) return NotFound("Order not found");

		if (orderFromDb.Status == OrderStatuses.Approved) {
			orderFromDb.Status = OrderStatuses.Shipped;
			_db.SaveChanges();
			return Ok("order shipped");
		}
		else {
			return BadRequest("order not shipped");
		}
	}

	[HttpPut("deliver/{id}")]
	public ActionResult Deliver(int id) {
		var orderFromDb = _db.Orders.Find(id);
		if (orderFromDb == null) return NotFound("Order not found");

		if (orderFromDb.Status == OrderStatuses.Shipped) {
			orderFromDb.Status = OrderStatuses.Delivered;
			_db.SaveChanges();
			return Ok("order Delivered");
		}
		else {
			return BadRequest("order not delivered");
		}
	}

	[HttpPut("return/{id}")]
	public ActionResult Return(int id, [FromQuery] bool restock = true) {
		var orderFromDb = _db.Orders.Find(id);
		if (orderFromDb == null) return NotFound("Order not found");

		if (!(orderFromDb.Status != OrderStatuses.Delivered || orderFromDb.Status != OrderStatuses.Shipped))
			return BadRequest("can't return order unless shipped or delivered");
		orderFromDb.Status = OrderStatuses.Returned;

		if (!restock) {
			_db.SaveChanges();
			return Ok("order returned without restocking");
		}

		var orderItems = _db.OrderItems.Where(x => x.OrderId == id).ToList();
		foreach (var orderItem in orderItems) {
			var product = _db.Products.Find(orderItem.ProductId);
			if (product == null) return NotFound("Product not found");
			product.Quantity += orderItem.Quantity;

			var stockAdjustment = new StockAdjustment {
				Change = orderItem.Quantity,
				Reason = "Returned order",
				Product_Id = orderItem.ProductId,
				Related_Order_Id = orderItem.OrderId,
			};
			_db.StockAdjustment.Add(stockAdjustment);
		}

		_db.SaveChanges();
		return Ok("order returned and restocked");
	}

	[HttpPut("cancel/{id}")]
	public ActionResult CancelOrder(int id, [FromQuery] bool restock = true) {
		var orderFromDb = _db.Orders.Find(id);
		if (orderFromDb == null) return NotFound("Order not found");

		if (orderFromDb.Status != OrderStatuses.Approved)
			return BadRequest("can't return order unless approved");
		orderFromDb.Status = OrderStatuses.Cancelled;

		if (!restock) {
			_db.SaveChanges();
			return NoContent();
		}

		List<OrderItem> orderItems = _db.OrderItems.Where(x => x.OrderId == id).ToList();
		foreach (var item in orderItems) {
			var product = _db.Products.Find(item.ProductId);
			var ItemStock = new StockAdjustment {
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

	[HttpDelete("{id}")]
	public ActionResult Delete(int id) {
		var order = _db.Orders.Find(id);
		if (order == null) return NotFound("Order not found");

		List<OrderItem> orderItems = _db.OrderItems.Where(x => x.OrderId == id).ToList();

		// NOTE: Uncomment the following code block if you want to restock items when an order is deleted.
		// However, this might not be the desired behavior in all scenarios.

		// Restock items if order is not already cancelled

		// if (order.Status != OrderStatuses.Cancelled) {
		// 	order.Status = OrderStatuses.Cancelled;

		// 	foreach (var item in orderItems) {
		// 		var product = _db.Products.Find(item.ProductId);
		// 		var ItemStock = new StockAdjustment {
		// 			Change = item.Quantity,
		// 			Reason = "Cancelled order",
		// 			Product_Id = item.ProductId,
		// 			Related_Order_Id = item.OrderId,
		// 		};
		// 		product.Quantity += item.Quantity;
		// 		_db.StockAdjustment.Add(ItemStock);
		// 	}
		// }

		_db.OrderItems.RemoveRange(orderItems);
		_db.Orders.Remove(order);
		_db.SaveChanges();
		return NoContent();
	}
}

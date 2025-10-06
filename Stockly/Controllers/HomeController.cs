using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Stockly.DTOs;

namespace Stockly.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController(AppDbContext db) : ControllerBase {
		[HttpGet]
		public IActionResult Get() {
			var productsCount = db.Products.Count();
			var ordersCount = db.Orders.Count();
			var pendingOrdersCount = db.Orders.Count(o => o.Status == "Pending");
			var unShippedOrdersCount = db.Orders
			.Count(o => o.Status != "Shipped" || o.Status != "Delivered" || o.Status != "Cancelled" || o.Status != "Returned");

			var productsStorage = db.Products
			.OrderByDescending(p => p.Quantity)
			.Select(p => new {
				Name = p.Name,
				Quantity = p.Quantity
			})
			.Take(5)
			.ToList();

			productsStorage.Add(new {
				Name = "Other Products",
				Quantity = db.Products.Sum(p => p.Quantity) - productsStorage.Sum(p => p.Quantity)
			});

			var latestOrders = db.Orders.Include(u => u.Items)
			.OrderByDescending(o => o.CreatedAt)
			.Select(o => new OrderDto {
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
			}).Take(5).ToList();

			var mostSoldProducts = db.OrderItems
			.GroupBy(oi => oi.ProductId)
			.Select(g => new {
				Product_id = g.Key,
				Product_name = g.First().Product.Name,
				TotalSold = g.Sum(oi => oi.Quantity)
			})
			.OrderByDescending(g => g.TotalSold)
			.Take(5)
			.ToList();

			return Ok(new {
				ProductsCount = productsCount,
				OrdersCount = ordersCount,
				UnShippedOrdersCount = unShippedOrdersCount,
				PendingOrdersCount = pendingOrdersCount,
				ProductsStorage = productsStorage,
				LatestOrders = latestOrders,
				MostSoldProducts = mostSoldProducts
			});
		}
	}
}

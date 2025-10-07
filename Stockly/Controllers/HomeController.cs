using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Stockly.DTOs;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(AppDbContext db) : ControllerBase {
	[HttpGet]
	public async Task<IActionResult> Get() {
		// Get counts
		int productsCount = await db.Products.CountAsync();
		int ordersCount = await db.Orders.CountAsync();
		int pendingOrdersCount = await db.Orders.CountAsync(o => o.Status == "Pending");
		int unshippedOrdersCount = await db.Orders.CountAsync(o =>
			o.Status != "Shipped" &&
			o.Status != "Delivered" &&
			o.Status != "Cancelled" &&
			o.Status != "Returned"
		);

		// Products storage
		var topProducts = await db.Products
			.OrderByDescending(p => p.Quantity)
			.Select(p => new { p.Name, p.Quantity })
			.Take(5)
			.ToListAsync();

		int totalQuantity = await db.Products.SumAsync(p => p.Quantity);
		int otherProductsQuantity = totalQuantity - topProducts.Sum(p => p.Quantity);

		var productsStorage = topProducts
			.Append(new { Name = "Other Products", Quantity = otherProductsQuantity })
			.ToList();

		// Latest orders
		var latestOrders = await db.Orders
			.Include(o => o.Items)
			.OrderByDescending(o => o.CreatedAt)
			.Take(5)
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
					UnitPrice = i.Price
				}).ToList()
			})
			.ToListAsync();

		// Most sold products
		var mostSoldProducts = await db.OrderItems
			.GroupBy(oi => oi.ProductId)
			.Select(g => new {
				Product_id = g.Key,
				Product_name = g.First().Product.Name,
				TotalSold = g.Sum(oi => oi.Quantity)
			})
			.OrderByDescending(g => g.TotalSold)
			.Take(5)
			.ToListAsync();

		// Orders per month
		var ordersPerMonth = await db.Orders
			.GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
			.Select(g => new {
				year = g.Key.Year,
				month = g.Key.Month,
				month_name = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
				orders_count = g.Count()
			})
			.OrderBy(g => g.year).ThenBy(g => g.month)
			.ToListAsync();

		return Ok(new {
			ProductsCount = productsCount,
			OrdersCount = ordersCount,
			UnShippedOrdersCount = unshippedOrdersCount,
			PendingOrdersCount = pendingOrdersCount,
			ProductsStorage = productsStorage,
			LatestOrders = latestOrders,
			MostSoldProducts = mostSoldProducts,
			OrdersPerMonth = ordersPerMonth
		});
	}
}

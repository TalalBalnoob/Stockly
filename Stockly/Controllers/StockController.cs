using Microsoft.AspNetCore.Mvc;

using Stockly.DTOs;
using Stockly.Models;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController(AppDbContext _db) : Controller {

	[HttpGet]
	public ActionResult<StockDto[]> Get() {
		var stock = _db.StockAdjustment
		   .Select(s => new StockDto {
			   Id = s.Id,
			   Change = s.Change,
			   Reason = s.Reason,
			   Product_Id = s.Product_Id,
			   Related_Order_Id = s.Related_Order_Id ?? null,
			   CreatedAt = s.CreatedAt
		   }).ToList();

		return Ok(stock);
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Stockly.DTOs;
using Stockly.Models;
using Stockly.Services;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController(AppDbContext _db) : Controller {
	public async Task<PagedResult<StockDto>> GetStocksAsync(PaginationParams paginationParams) {
		var query = _db.StockAdjustment.AsQueryable();

		var totalCount = query.Count();

		var items = query
			.OrderByDescending(p => p.CreatedAt)
			.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
			.Take(paginationParams.PageSize)
			.Include(s => s.Product)
			.Select(s =>
			 new StockDto {
				 Id = s.Id,
				 Change = s.Change,
				 Reason = s.Reason,
				 Product_Id = s.Product_Id,
				 Product_Name = s.Product != null ? s.Product.Name : null,
				 Related_Order_Id = s.Related_Order_Id ?? null,
				 CreatedAt = s.CreatedAt
			 }).ToList();

		return new PagedResult<StockDto>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
	}

	[HttpGet]
	public ActionResult<StockDto[]> Get([FromQuery] PaginationParams paginationParams) {
		var stock = GetStocksAsync(paginationParams).Result;

		return Ok(stock);
	}
}

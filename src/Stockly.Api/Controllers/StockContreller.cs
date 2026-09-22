using Microsoft.AspNetCore.Mvc;

using Stockly.Application.DTOs.StockAdjustments;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.StockAdjustment;

namespace Stockly.Application.UseCases.StockAdjustment;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase {
	private readonly IAdjustStockUseCase _adjustStockUseCase;
	private readonly IStockAdjustmentsRepo _stockAdjustmentsRepo;

	public StockController(IAdjustStockUseCase adjustStockUseCase, IStockAdjustmentsRepo stockAdjustmentsRepo) {
		_adjustStockUseCase = adjustStockUseCase;
		_stockAdjustmentsRepo = stockAdjustmentsRepo;
	}

	[HttpGet("{productId}/stock")]
	public async Task<IActionResult> GetStock(Guid productId) {
		try {
			var stockAdjustments = await _stockAdjustmentsRepo.GetByProductIdAsync(productId);
			if (stockAdjustments == null || !stockAdjustments.Any()) {
				return NotFound(new { message = "No stock adjustments found for the specified product." });
			}

			var currentStock = stockAdjustments.Sum(sa => sa.Change);
			return Ok(new { ProductId = productId, CurrentStock = currentStock });
		}
		catch (Exception ex) {
			return StatusCode(500, new { message = "An error occurred while retrieving stock information.", details = ex.Message });
		}
	}

	[HttpPost("{productId}/set-stock")]
	public async Task<IActionResult> AdjustStock(Guid productId, [FromBody] CreateStockAdjustmentDto request) {
		try {
			request.ProductId = productId;
			var response = await _adjustStockUseCase.ExecuteAsync(request);
			return Ok(response);
		}
		catch (ArgumentException ex) {
			return NotFound(new { message = ex.Message });
		}
		catch (Exception ex) {
			return StatusCode(500, new { message = "An error occurred while adjusting stock.", details = ex.Message });
		}
	}
}

using Microsoft.AspNetCore.Mvc;

using Stockly.Application.DTOs.StockAdjustments;
using Stockly.Application.Interfaces.UseCases.StockAdjustment;

namespace Stockly.Application.UseCases.StockAdjustment;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase {
	private readonly IAdjustStockUseCase _adjustStockUseCase;

	public StockController(IAdjustStockUseCase adjustStockUseCase) {
		_adjustStockUseCase = adjustStockUseCase;
	}

	[HttpPost("{productId}/set-stock")]
	public async Task<IActionResult> AdjustStock([FromBody] CreateStockAdjustmentDto request) {
		try {
			request.ProductId = Guid.Parse(RouteData.Values["productId"].ToString() ?? throw new ArgumentNullException("Product ID is missing in the route."));
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

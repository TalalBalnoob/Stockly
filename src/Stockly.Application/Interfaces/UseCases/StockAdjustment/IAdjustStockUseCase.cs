using Stockly.Application.DTOs.StockAdjustments;

namespace Stockly.Application.Interfaces.UseCases.StockAdjustment;

public interface IAdjustStockUseCase {
	Task<StockAdjustmentResponseDto> ExecuteAsync(CreateStockAdjustmentDto request);
}

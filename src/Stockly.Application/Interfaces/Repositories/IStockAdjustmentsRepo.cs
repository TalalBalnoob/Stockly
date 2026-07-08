using Stockly.Application.DTOs.StockAdjustments;

namespace Stockly.Application.Interfaces.Repositories;

public interface IStockAdjustmentsRepo
{
    // Queries
    Task<IEnumerable<StockAdjustmentResponseDto>> GetAllAsync();
    Task<StockAdjustmentResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<StockAdjustmentResponseDto>> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<StockAdjustmentResponseDto>> GetByRelatedOrderIdAsync(Guid orderId);

    // Commands
    Task<StockAdjustmentResponseDto> AddAsync(CreateStockAdjustmentDto dto);

    // Checks
    Task<bool> ExistsAsync(Guid id);
}

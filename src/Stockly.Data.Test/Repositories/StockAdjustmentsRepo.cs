using System.Collections.ObjectModel;

using Stockly.Application.DTOs.StockAdjustments;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Domain.Entities;

namespace Stockly.Data.Repositories;

public class StockAdjustmentsRepo : IStockAdjustmentsRepo {
	private readonly Collection<StockAdjustment> _context;

	public StockAdjustmentsRepo(Collection<StockAdjustment> context) {
		_context = context;
	}

	public async Task<IEnumerable<StockAdjustmentResponseDto>> GetAllAsync() {
		return _context
			.Select(sa => MapToResponseDto(sa))
			.ToList();
	}

	public async Task<StockAdjustmentResponseDto?> GetByIdAsync(Guid id) {
		var adjustment = _context
			.First(sa => sa.Id == id);

		return adjustment != null ? MapToResponseDto(adjustment) : null;
	}

	public async Task<IEnumerable<StockAdjustmentResponseDto>> GetByProductIdAsync(Guid productId) {
		return _context
			.Where(sa => sa.ProductId == productId)
			.Select(sa => MapToResponseDto(sa))
			.ToList();
	}

	public async Task<IEnumerable<StockAdjustmentResponseDto>> GetByRelatedOrderIdAsync(Guid orderId) {
		return _context
			.Where(sa => sa.RelatedOrderId == orderId)
			.Select(sa => MapToResponseDto(sa))
			.ToList();
	}

	public async Task<StockAdjustmentResponseDto> AddAsync(StockAdjustment stockAdjustment) {
		_context.Add(stockAdjustment);

		return MapToResponseDto(stockAdjustment);
	}

	public async Task<bool> ExistsAsync(Guid id) {
		return _context.Any(sa => sa.Id == id);
	}

	private static StockAdjustmentResponseDto MapToResponseDto(StockAdjustment adjustment) {
		return new StockAdjustmentResponseDto {
			Id = adjustment.Id,
			ProductId = adjustment.ProductId,
			ProductName = adjustment.Product?.Name ?? "Unknown Product",
			RelatedOrderId = adjustment.RelatedOrderId,
			Change = adjustment.Change,
			Reason = adjustment.Reason,
			AdjustmentDate = adjustment.AdjustmentDate
		};
	}
}

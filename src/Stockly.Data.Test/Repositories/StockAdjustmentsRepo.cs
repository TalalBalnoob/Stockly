using Microsoft.EntityFrameworkCore;

using Stockly.Application.DTOs.StockAdjustments;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Data.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Data.Repositories;

public class StockAdjustmentsRepo : IStockAdjustmentsRepo {
	private readonly StocklyDbContext _context;

	public StockAdjustmentsRepo(StocklyDbContext context) {
		_context = context;
	}

	public async Task<IEnumerable<StockAdjustmentResponseDto>> GetAllAsync() {
		return await _context.StockAdjustments
			.Include(sa => sa.Product)
			.Select(sa => MapToResponseDto(sa))
			.ToListAsync();
	}

	public async Task<StockAdjustmentResponseDto?> GetByIdAsync(Guid id) {
		var adjustment = await _context.StockAdjustments
			.Include(sa => sa.Product)
			.FirstOrDefaultAsync(sa => sa.Id == id);

		return adjustment != null ? MapToResponseDto(adjustment) : null;
	}

	public async Task<IEnumerable<StockAdjustmentResponseDto>> GetByProductIdAsync(Guid productId) {
		return await _context.StockAdjustments
			.Include(sa => sa.Product)
			.Where(sa => sa.ProductId == productId)
			.Select(sa => MapToResponseDto(sa))
			.ToListAsync();
	}

	public async Task<IEnumerable<StockAdjustmentResponseDto>> GetByRelatedOrderIdAsync(Guid orderId) {
		return await _context.StockAdjustments
			.Include(sa => sa.Product)
			.Where(sa => sa.RelatedOrderId == orderId)
			.Select(sa => MapToResponseDto(sa))
			.ToListAsync();
	}

	public async Task<StockAdjustmentResponseDto> AddAsync(StockAdjustment stockAdjustment) {
		var product = await _context.Products.FindAsync(stockAdjustment.ProductId);
		if (product == null) {
			throw new KeyNotFoundException($"Product with ID {stockAdjustment.ProductId} not found.");
		}

		_context.StockAdjustments.Add(stockAdjustment);
		await _context.SaveChangesAsync();

		return MapToResponseDto(stockAdjustment);
	}

	public async Task<bool> ExistsAsync(Guid id) {
		return await _context.StockAdjustments.AnyAsync(sa => sa.Id == id);
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

using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class StockAdjustmentService(IStockAdjustmentRepository adjustmentRepo)
	: IStockAdjustmentService {
	public async Task<StockAdjustment> AddAdjustment(NewAsjustmentDto adjustmentDto) {
		var adjustment = new StockAdjustment {
			Id = Guid.NewGuid(),
			ProductId = adjustmentDto.ProductId,
			OrderId = adjustmentDto.OrderId ?? null,
			Quantity = adjustmentDto.Quantity,
			Reason = adjustmentDto.Reason,
			CreatedAt = DateTime.UtcNow,
		};

		return await adjustmentRepo.AddAsync(adjustment);
	}

	public async Task<List<StockAdjustment>> GetAllAdjustments() {
		var adjustments = await adjustmentRepo.GetAllAsync();
		return adjustments.ToList();
	}

	public async Task<StockAdjustment?> GetAdjustmentById(Guid id) {
		return await adjustmentRepo.GetByIdAsync(id);
	}

	public async Task<List<StockAdjustment>> GetAdjustmentsByProductId(Guid productId) {
		return await adjustmentRepo.GetByProductIdAsync(productId);
	}

	// TODO Keep an eye on this
	public async Task<StockAdjustment> UpdateAdjustment(StockAdjustment adjustment) {
		return await adjustmentRepo.UpdateAsync(adjustment);
	}

	public async Task Delete(Guid id) {
		adjustmentRepo.DeleteAsync(id);
		await Task.CompletedTask;
	}
}
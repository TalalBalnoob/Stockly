using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class StockAdjustmentService(IStockAdjustmentRepository adjustmentRepo)
    : IStockAdjustmentService
{
    public async Task<StockAdjustment> AddAdjustment(NewAsjustmentDto adjustmentDto)
    {
        var adjustment = new StockAdjustment
        {
            Id = Guid.NewGuid(),
            ProductId = adjustmentDto.ProductId,
            OrderId = adjustmentDto.OrderId,
            Quantity = adjustmentDto.Quantity,
            Reason = adjustmentDto.Reason,
            CreatedAt = DateTime.UtcNow,
        };

        return await adjustmentRepo.AddAsync(adjustment);
    }
}


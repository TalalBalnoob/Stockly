using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;

namespace Stockly.Application.Services;

public class StockAdjustmentService(IStockAdjustmentRepository adjustmentRepo) : IStockAdjustmentService {
}
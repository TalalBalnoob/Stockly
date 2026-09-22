using Stockly.Application.DTOs.StockAdjustments;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.StockAdjustment;

namespace Stockly.Application.UseCases.StockAdjustment;

public class AdjustStockUseCase : IAdjustStockUseCase {
	private readonly IStockAdjustmentsRepo _stockRepository;
	private readonly IProductsRepo _productsRepository;

	public AdjustStockUseCase(IStockAdjustmentsRepo stockRepository, IProductsRepo productsRepository) {
		_stockRepository = stockRepository;
		_productsRepository = productsRepository;
	}

	public async Task<StockAdjustmentResponseDto> ExecuteAsync(CreateStockAdjustmentDto request) {
		var product = await _productsRepository.GetByIdAsync(request.ProductId);
		if (product == null) {
			throw new ArgumentException($"Product with ID {request.ProductId} not found.");
		}
		if (product.Quantity + request.Change < 0) {
			throw new ArgumentException("Stock change would result in negative inventory.");
		}

		var stockAdjustment = new Domain.Entities.StockAdjustment {
			ProductId = request.ProductId,
			RelatedOrderId = request.RelatedOrderId,
			Change = request.Change,
			Reason = request.Reason,
		};

		await _stockRepository.AddAsync(stockAdjustment);

		product.Quantity += request.Change;
		await _productsRepository.UpdateAsync(product);

		return new StockAdjustmentResponseDto {
			Id = stockAdjustment.Id,
			ProductId = stockAdjustment.ProductId,
			RelatedOrderId = stockAdjustment.RelatedOrderId,
			Change = stockAdjustment.Change,
			Reason = stockAdjustment.Reason,
		};
	}
}



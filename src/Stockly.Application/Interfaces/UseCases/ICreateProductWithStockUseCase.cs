using Stockly.Application.DTOs;

namespace Stockly.Application.Interfaces.UseCases;

public interface ICreateProductWithStockUseCase {
	Task<NewCreatedProductDto> Execute(NewProductDto productDto);
}
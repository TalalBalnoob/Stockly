using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;

namespace Stockly.Application.UseCases.CreateProductWithStock;

public class CreateProductWithStockUseCase(IProductRepository productRepo, IStockRepository stockRepo) {
	public async Task<NewCreatedProductDto> Execute(NewProductDto productDto) {
		var newProduct = await productRepo.AddAsync(new() {
			Id = new Guid(),
			Name = productDto.Name,
			Price = productDto.Price,
			IsActive = productDto.IsActive ?? true,
			Description = productDto.Description ?? string.Empty,
		});

		// create stock for the product
		var newStock = await stockRepo.AddAsync(new() {
			ProductId = newProduct.Id,
			Quantity = productDto.InialQuantity,
			StorageNote = $"Product {newProduct.Id} has been added",
		});

		// return both the product and the stock 
		return new NewCreatedProductDto() {
			Product = newProduct,
			Stock = newStock
		};
	}
}
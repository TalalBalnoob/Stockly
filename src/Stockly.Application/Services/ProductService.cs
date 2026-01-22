using System.Runtime.InteropServices.JavaScript;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class ProductService(IProductRepository productRepo, IStockRepository stockRepo) : IProductService {
	public async Task<IEnumerable<Product>> GetAllProducts() {
		return await productRepo.GetAllAsync();
	}

	public async Task<Product?> GetProductById(Guid id) {
		var product = await productRepo.GetByIdAsync(id);
		if (product == null) throw new Exception("Product not found");

		return product;
	}

	public async Task<Product?> GetProductByName(string name) {
		var product = await productRepo.GetByNameAsync(name);
		if (product == null) throw new Exception("Product not found");

		return product;
	}

	public async Task<IEnumerable<Product>> GetUnavilabelProducts() {
		var allProducts = await productRepo.GetAllAsync();
		return allProducts.Where(p => !p.IsActive).ToList();
	}

	// is this the right place for this method?
	public async Task<IEnumerable<Product>> GetOutOfStockProducts() {
		var allStocks = await stockRepo.GetAllAsync();
		var outOfStockProductsIds = allStocks.Where(s => s.Quantity < 1).Select(s => s.Id).ToList();

		var products = new List<Product>();
		foreach (var id in outOfStockProductsIds) {
			var product = await productRepo.GetByIdAsync(id);
			if (product == null) continue;
			products.Add(product);
		}

		return products;
	}

	public async Task<NewCreatedProductDto> AddProduct(NewProductDto productDto) {
		// create the product 
		var newProduct = await productRepo.AddAsync(new() {
			Id = Guid.NewGuid(),
			Name = productDto.Name,
			Price = productDto.Price,
			IsActive = productDto.IsActive ?? true,
			Description = productDto.Description ?? string.Empty,
		});

		// create stock for the product
		// replace with the add method from stock service later
		var newStock = await stockRepo.AddAsync(new() {
			ProductId = newProduct.Id,
			Quantity = productDto.InialQuantity,
			StorageNote = "",
		});

		// return both the product and the stock 
		return new NewCreatedProductDto() {
			Product = newProduct,
			Stock = newStock
		};
	}

	public async Task<Product> UpdateProduct(Product product) {
		var productFromDb = await productRepo.GetByIdAsync(product.Id);
		if (productFromDb == null) throw new Exception("Product not found");

		productFromDb.Name = product.Name;
		productFromDb.Price = product.Price;
		productFromDb.IsActive = product.IsActive;
		productFromDb.Description = product.Description;

		return await productRepo.UpdateAsync(productFromDb);
	}

	public async Task DeleteProduct(Guid id) {
		var product = await productRepo.GetByIdAsync(id);
		if (product == null) throw new Exception("Product not found");

		productRepo.DeleteAsync(id);
	}
}
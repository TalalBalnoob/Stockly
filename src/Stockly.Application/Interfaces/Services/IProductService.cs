using Stockly.Application.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.Services;

public interface IProductService {
	Task<IEnumerable<Product>> GetAllProducts();
	Task<IEnumerable<Product>> GetAllProductsWithStocks();
	Task<Product?> GetProductById(Guid id);
	Task<Product?> GetProductWithStocksById(Guid id);
	Task<Product?> GetProductByName(string name);
	Task<Product?> GetProductWithStocksByName(string name);
	Task<Product?> GetProductWithStockByName(string name);
	Task<IEnumerable<Product>> GetUnavilabelProducts();
	Task<IEnumerable<Product>> GetOutOfStockProducts();
	Task<Product> AddProduct(NewProductDto productDto);
	Task<Product> UpdateProduct(Product product);
	Task DeleteProduct(Guid id);
}
using System.Collections.ObjectModel;

using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Domain.Entities;

namespace Stockly.Data.Repositories;

public class ProductsRepo : IProductsRepo {
	private readonly Collection<Product> _context;

	public ProductsRepo(Collection<Product> context) {
		_context = context;
	}

	public async Task<IEnumerable<Product>> GetAllAsync() {
		return _context.ToList();
	}

	public async Task<Product?> GetByIdAsync(Guid id) {
		var product = _context.First(p => p.Id == id);
		return product != null ? product : null;
	}

	public async Task<IEnumerable<Product>> GetByNameAsync(string name) {
		return _context.Where(p => p.Name == name).ToList();
	}

	public async Task<IEnumerable<Product>> GetAvailableAsync() {
		return _context
			.Where(p => p.IsAvailable)
			.ToList();
	}

	public async Task<IEnumerable<Product>> GetOutOfStockAsync() {
		return _context
			.Where(p => p.Quantity <= 0)
			.ToList();
	}

	public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice) {
		return _context
			.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
			.ToList();
	}

	public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold) {
		return _context
			.Where(p => p.Quantity <= threshold)
			.ToList();
	}

	public async Task<Product> AddAsync(Product product) {
		_context.Add(product);
		return product;
	}

	public async Task<Product> UpdateAsync(Product product) {
		int index = _context.IndexOf(product);
		Product searchedProduct = _context.First(p => p.Id == product.Id);
		searchedProduct.Name = product.Name;
		searchedProduct.Price = product.Price;
		searchedProduct.Quantity = product.Quantity;
		searchedProduct.Description = product.Description;
		searchedProduct.StorageNote = product.StorageNote;
		searchedProduct.IsAvailable = product.IsAvailable;

		this._context.RemoveAt(index);
		this._context.Insert(index,searchedProduct);

		return searchedProduct;
	}

	public async Task DeleteAsync(Guid id) {
		var product =  _context.First(p=> p.Id == id);
		if (product != null) {
			_context.Remove(product);
		}
	}

	public async Task<bool> ExistsAsync(Guid id) {
		return _context.Any(p => p.Id == id);
	}


	public ProductResponseDto MapToResponseDto(Product product) {
		return new ProductResponseDto {
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			Quantity = product.Quantity,
			Description = product.Description,
			StorageNote = product.StorageNote,
			IsAvailable = product.IsAvailable,
			CreatedAt = product.CreatedAt
		};
	}
}

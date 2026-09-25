using Microsoft.EntityFrameworkCore;

using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Data.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Data.Repositories;

public class ProductsRepo : IProductsRepo {
	private readonly StocklyDbContext _context;

	public ProductsRepo(StocklyDbContext context) {
		_context = context;
	}

	public async Task<IEnumerable<Product>> GetAllAsync() {
		return await _context.Products
			.ToListAsync();
	}

	public async Task<Product?> GetByIdAsync(Guid id) {
		var product = await _context.Products.FindAsync(id);
		return product != null ? product : null;
	}

	public async Task<IEnumerable<Product>> GetByNameAsync(string name) {
		return await _context.Products
			.Where(p => p.Name.Contains(name))
			.ToListAsync();
	}

	public async Task<IEnumerable<Product>> GetAvailableAsync() {
		return await _context.Products
			.Where(p => p.IsAvailable)
			.ToListAsync();
	}

	public async Task<IEnumerable<Product>> GetOutOfStockAsync() {
		return await _context.Products
			.Where(p => p.Quantity <= 0)
			.ToListAsync();
	}

	public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice) {
		return await _context.Products
			.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
			.ToListAsync();
	}

	public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold) {
		return await _context.Products
			.Where(p => p.Quantity <= threshold)
			.ToListAsync();
	}

	public async Task<Product> AddAsync(Product product) {
		_context.Products.Add(product);
		await _context.SaveChangesAsync();

		return product;
	}

	public async Task<Product> UpdateAsync(Product product) {
		_context.Products.Update(product);
		await _context.SaveChangesAsync();

		return product;
	}

	public async Task DeleteAsync(Guid id) {
		var product = await _context.Products.FindAsync(id);
		if (product != null) {
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
		}
	}

	public async Task<bool> ExistsAsync(Guid id) {
		return await _context.Products.AnyAsync(p => p.Id == id);
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

using Microsoft.EntityFrameworkCore;
using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Data.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Data.Repositories;

public class ProductsRepo : IProductsRepo
{
    private readonly StocklyDbContext _context;

    public ProductsRepo(StocklyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        return await _context.Products
            .Select(p => MapToResponseDto(p))
            .ToListAsync();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        return product != null ? MapToResponseDto(product) : null;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetByNameAsync(string name)
    {
        return await _context.Products
            .Where(p => p.Name.Contains(name))
            .Select(p => MapToResponseDto(p))
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAvailableAsync()
    {
        return await _context.Products
            .Where(p => p.IsAvailable)
            .Select(p => MapToResponseDto(p))
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductResponseDto>> GetOutOfStockAsync()
    {
        return await _context.Products
            .Where(p => p.Quantity <= 0)
            .Select(p => MapToResponseDto(p))
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductResponseDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .Select(p => MapToResponseDto(p))
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductResponseDto>> GetLowStockAsync(int threshold)
    {
        return await _context.Products
            .Where(p => p.Quantity <= threshold)
            .Select(p => MapToResponseDto(p))
            .ToListAsync();
    }

    public async Task<ProductResponseDto> AddAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            Description = dto.Description,
            StorageNote = dto.StorageNote,
            IsAvailable = dto.IsAvailable,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        if (dto.Name != null) product.Name = dto.Name;
        product.Price = dto.Price;
        product.Quantity = dto.Quantity;
        if (dto.Description != null) product.Description = dto.Description;
        if (dto.StorageNote != null) product.StorageNote = dto.StorageNote;
        product.IsAvailable = dto.IsAvailable;

        await _context.SaveChangesAsync();

        return MapToResponseDto(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    private static ProductResponseDto MapToResponseDto(Product product)
    {
        return new ProductResponseDto
        {
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

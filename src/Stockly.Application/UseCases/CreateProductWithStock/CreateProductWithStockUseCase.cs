using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases.CreateProductWithStock;

public class CreateProductWithStockUseCase(IProductRepository productRepo)
    : ICreateProductWithStockUseCase
{
    public async Task<Product> Execute(NewProductDto productDto)
    {
        var newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Price = productDto.Price,
            IsActive = productDto.IsActive ?? true,
            Description = productDto.Description ?? string.Empty,
        };

        // create stock for the product
        var newStock = new Stock()
        {
            Id = Guid.NewGuid(),
            ProductId = newProduct.Id,
            Quantity = productDto.InialQuantity,
            StorageNote = $"Product {newProduct.Id} has been added",
        };

        newProduct.Stock = newStock;

        await productRepo.AddAsync(newProduct);

        // return both the product and the stock
        return newProduct;
    }
}


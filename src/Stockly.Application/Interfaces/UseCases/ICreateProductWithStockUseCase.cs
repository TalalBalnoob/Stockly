using Stockly.Application.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.UseCases;

public interface ICreateProductWithStockUseCase
{
    Task<Product> Execute(NewProductDto productDto);
}


using Stockly.Domain.DTOs;

namespace Stockly.Application.Interfaces.UseCases;

public interface ICreateProductUseCase {
    Task Execute(NewProductDTO newProduct);
}

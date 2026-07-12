namespace Stockly.Application.Interfaces.UseCases.Products;

public interface IDeleteProductUseCase {
	Task ExecuteAsync(Guid productId);
}

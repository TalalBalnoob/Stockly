namespace Stockly.Application.Interfaces.UseCases;

public interface IDeleteOrderUseCase {
	Task Execute(Guid id);
}
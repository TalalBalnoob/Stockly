namespace Stockly.Application.Interfaces.UseCases;

public interface IDeleteProductAndStock {
	Task Execute(Guid id);
}
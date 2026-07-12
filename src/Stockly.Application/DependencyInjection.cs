using Microsoft.Extensions.DependencyInjection;

using Stockly.Application.Interfaces.UseCases.Products;
using Stockly.Application.UseCases.Products;


namespace Stockly.Data;

public static class DependencyInjection {
	public static IServiceCollection AddApplication(
		this IServiceCollection services
		) {

		// Products Use Cases
		services.AddScoped<IGetProductsUseCase, GetProductsUseCase>();
		services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
		services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();

		return services;
	}
}

using NUnit.Framework;
using Stockly.Application.DTOs;
using Stockly.Application.Services;
using Stockly.Domain.Entity;
using Stockly.Infrastructure.Repositories;
using Stockly.Infrastructure.Test;

namespace Stockly.Application.Test;

public class ProductServiceTests {
	private ProductService _service = null!;

	private FakeProductRepository _productRepo = null!;
	private FakeStockRepository _stockRepo = null!;

	[SetUp]
	public void Setup() {
		_productRepo = new FakeProductRepository();
		_stockRepo = new FakeStockRepository();

		_service = new ProductService(
			_productRepo,
			_stockRepo
		);
	}

	[Test]
	public async Task GetAllProducts_ReturnsAllProducts() {
		await _service.AddProduct(new NewProductDto {
			Name = "Laptop",
			Price = 1000,
			Description = "",
			IsActive = true,
			InialQuantity = 10
		});

		var result = await _service.GetAllProducts();

		Assert.That(result.First().Name, Is.EqualTo("Laptop"));
		await _service.DeleteProduct(result.First().Id);
		result = await _service.GetAllProducts();
		Assert.That(result.Count(), Is.EqualTo(0));
	}
}
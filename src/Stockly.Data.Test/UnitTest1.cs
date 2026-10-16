using Stockly.Application.DTOs.Orders;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.UseCases.Orders;
using Stockly.Application.UseCases.StockAdjustment;
using Stockly.Data.Test.Repositories;
using Stockly.Domain.Entities;
using Stockly.Domain.Enums;

namespace Stockly.Data.Test;


public class Tests {
	private FakeOrdersRepo _orderRepository;
	private FakeProductsRepo _productsRepository;
	private FakeStockAdjustmentsRepo _stockRepository;

	private AdjustStockUseCase _StockAdjusmntUseCase;
	private CreateOrderUseCase _CreateOrderUseCase;
    [SetUp]
    public void Setup() {
	    this._orderRepository = new FakeOrdersRepo([]);
	    this._productsRepository = new FakeProductsRepo([]);
	    this._stockRepository = new FakeStockAdjustmentsRepo([]);

	    _StockAdjusmntUseCase = new AdjustStockUseCase(this._stockRepository, this._productsRepository);
	    _CreateOrderUseCase = new CreateOrderUseCase(this._orderRepository,this._productsRepository,this._stockRepository, _StockAdjusmntUseCase);
    }

    [Test]
    public async Task CreateOrder_WithEnoughStock_SavesOrder() {
	    // areng
	    var products = Enumerable.Range(1, 30)
		    .Select(i => new Product {
			    Id = Guid.NewGuid(),
			    Name = $"Product {i}",
			    Price = i * 10,
			    Quantity = 100
		    })
		    .ToList();

	    foreach (var p in products)
		    await _productsRepository.AddAsync(p);

	    var result = await _CreateOrderUseCase.ExecuteAsync(new CreateOrderRequest {
		    CustomerName = "Talal Balnoob",
		    CustomerContact = "536399473",
		    PaymentMethod = "MADA",
		    PaymentStatus = Payment_status.Paid,
		    Status = Order_status.Pending,
		    OrderItems = [
			    new CreateOrderItemRequest {
				    ProductId = products[0].Id,
				    Quantity = 10
			    },
			    new CreateOrderItemRequest {
				    ProductId = products[1].Id,
				    Quantity = 33
			    }
		    ],
		    ShippingAddress = "123 Main St, Anytown, USA",
		    PaymentReference = "1234567890",
		    CreatedAt = DateTime.UtcNow
	    });

	    Assert.That(result.Total, Is.EqualTo(760));
    }
}

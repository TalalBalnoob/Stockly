using Microsoft.AspNetCore.Mvc;

using Stockly.Application.DTOs.Orders;
using Stockly.Application.Interfaces.UseCases.Orders;

namespace Stockly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase {
	IGetAllOrdersUseCase _getAllOrdersUseCase;
	IGetOrderByIdUseCase _getOrderByIdUseCase;
	ICreateOrderUseCase _createOrderUseCase;

	public OrdersController(
		IGetAllOrdersUseCase getAllOrdersUseCase,
		IGetOrderByIdUseCase getOrderByIdUseCase,
		ICreateOrderUseCase createOrderUseCase
		) {
		_getAllOrdersUseCase = getAllOrdersUseCase;
		_getOrderByIdUseCase = getOrderByIdUseCase;
		_createOrderUseCase = createOrderUseCase;

	}

	[HttpGet]
	public async Task<IActionResult> GetOrders([FromQuery] OrderQueryParams query) {
		return Ok(await _getAllOrdersUseCase.ExecuteAsync(query));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetOrdersById(Guid id) {
		return Ok(await _getOrderByIdUseCase.ExecuteAsync(id));
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrders([FromBody] CreateOrderRequest order) {
		var createdProduct = await _createOrderUseCase.ExecuteAsync(order);
		return CreatedAtAction(nameof(GetOrdersById), new { id = createdProduct.Id }, createdProduct);
	}

	// [HttpPut("{id}")]`
	// public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto product) {
	// 	await _updateProductUseCase.ExecuteAsync(id, product);
	// 	return NoContent();
	// }

	// [HttpDelete("{id}")]
	// public async Task<IActionResult> DeleteProduct(Guid id) {
	// 	await _deleteProductUseCase.ExecuteAsync(id);
	// 	return NoContent();
	// }
}

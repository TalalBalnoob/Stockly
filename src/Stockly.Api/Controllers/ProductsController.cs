using System.Net;

using Microsoft.AspNetCore.Mvc;

using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.UseCases.Products;

namespace Stockly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
class ProductsController : ControllerBase {
	IGetProductByIdUseCase _getProductByIdUseCase;
	IGetProductsUseCase _getProductsUseCase;
	ICreateProductUseCase _createProductUseCase;
	IUpdateProductUseCase _updateProductUseCase;
	IDeleteProductUseCase _deleteProductUseCase;

	public ProductsController(
		IGetProductByIdUseCase getProductByIdUseCase,
		IGetProductsUseCase getProductsUseCase,
		ICreateProductUseCase createProductUseCase,
		IUpdateProductUseCase updateProductUseCase,
		IDeleteProductUseCase deleteProductUseCase
		) {
		_getProductByIdUseCase = getProductByIdUseCase;
		_getProductsUseCase = getProductsUseCase;
		_createProductUseCase = createProductUseCase;
		_updateProductUseCase = updateProductUseCase;
		_deleteProductUseCase = deleteProductUseCase;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParams query) {
		return Ok(await _getProductsUseCase.ExecuteAsync(query));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductById(Guid id) {
		return Ok(await _getProductByIdUseCase.ExecuteAsync(id));
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product) {
		var createdProduct = await _createProductUseCase.ExecuteAsync(product);
		return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto product) {
		await _updateProductUseCase.ExecuteAsync(id, product);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(Guid id) {
		await _deleteProductUseCase.ExecuteAsync(id);
		return NoContent();
	}
}

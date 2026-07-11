using Microsoft.AspNetCore.Mvc;

using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.UseCases;

namespace Stockly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
class ProductsController : ControllerBase {
	IGetProductByIdUseCase _getProductByIdUseCase;
	IGetProductsUseCase _getProductsUseCase;

	public ProductsController(IGetProductByIdUseCase getProductByIdUseCase, IGetProductsUseCase getProductsUseCase) {
		_getProductByIdUseCase = getProductByIdUseCase;
		_getProductsUseCase = getProductsUseCase;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParams query) {
		return Ok(await _getProductsUseCase.ExecuteAsync(query));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductById(Guid id) {
		return Ok(await _getProductByIdUseCase.ExecuteAsync(id));
	}

}

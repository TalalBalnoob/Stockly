using Microsoft.AspNetCore.Mvc;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Domain.Entity;

namespace Stockly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(
	IProductService productService,
	IStockService stockService,
	ICreateProductWithStockUseCase productWithStockUseCase,
	IDeleteProductAndStock deleteProduct
) : ControllerBase {
	[HttpGet]
	public IActionResult GetAll() {
		return Ok(productService.GetAllProductsWithStocks());
	}

	[HttpGet("{id}")]
	public IActionResult GetOne(string id) {
		try {
			return Ok(productService.GetProductWithStocksById(Guid.Parse(id)));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("search")]
	public IActionResult GetByName([FromQuery] string text) {
		return Ok(productService.GetProductWithStocksByName(text));
	}

	[HttpPost]
	public async Task<IActionResult> Create(NewProductDto productDto) {
		try {
			return Ok(await productWithStockUseCase.Execute(productDto));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPut("{productId}/stock")]
	public async Task<IActionResult> ReStock(UpdateStockDto newStock) {
		try {
			await stockService.UpdateStock(newStock);
			return Ok(await productService.GetProductWithStocksById(newStock.ProductId));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> Update(Product product) {
		try {
			return Ok(await productService.UpdateProduct(product));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string id) {
		try {
			await deleteProduct.Execute(Guid.Parse(id));
			return NoContent();
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}
}
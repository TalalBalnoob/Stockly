using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Stockly.Application.Interfaces.UseCases;
using Stockly.Application.UseCases;
using Stockly.Domain.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(
	CreateProductUseCase createProductUseCase,
	UpdateProductUseCase updateProductUseCase,
	DeleteProductUseCase deleteProductUseCase)
	: ControllerBase {
	[HttpGet("{id}")]
	public IActionResult GetOne(Guid id) {
		return this.Ok();
	}

	[HttpGet]
	public IActionResult GetAll(
		[FromQuery] int? lessThen,
		[FromQuery] int? moreThen,
		[FromQuery] bool? outOfStock
	) {
		return this.Ok(new {
			LessThen = lessThen,
			MoreThen = moreThen,
			OutOfStock = outOfStock
		});
	}

	[HttpPost]
	public async Task<IActionResult> Create(NewProductDTO product) {
		Product createdProduct = await createProductUseCase.Execute(product);
		return this.Ok(createdProduct);
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateProductDTO productDto) {
		Product updatedProduct = await updateProductUseCase.Execute(productDto);
		return this.Ok(updatedProduct);
	}

	// Delete
	[HttpDelete("{id}")]
	public IActionResult Delete(string id) {
		deleteProductUseCase.Execute(id);
		return this.Ok();
	}
}

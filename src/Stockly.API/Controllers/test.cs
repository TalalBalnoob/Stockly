using Microsoft.AspNetCore.Mvc;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;

namespace Stockly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(IProductService productService, ICreateProductWithStockUseCase productWithStockUseCase)
	: ControllerBase {
	[HttpGet]
	public IActionResult Get() {
		return Ok(productService.GetAllProductsWithStocks());
	}

	[HttpGet("create")]
	public IActionResult Post() {
		productWithStockUseCase.Execute(new NewProductDto {
			Name = "Laptop",
			Price = 1000,
			Description = "",
			IsActive = true,
			InialQuantity = 10
		});
		return Ok();
	}
}
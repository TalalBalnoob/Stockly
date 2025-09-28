using Microsoft.AspNetCore.Mvc;

using Stockly.DTOs;
using Stockly.Models;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(AppDbContext _db) : Controller {
	[HttpGet]
	public ActionResult<Product[]> Index() {
		var products = _db.Products.ToList();
		return Ok(products);
	}

	[HttpGet("{id}")]
	public ActionResult<Product> Show(int id) {
		var product = _db.Products.FirstOrDefault(u => id == u.Id);
		if (product == null) return NotFound();

		return Ok(product);
	}

	[HttpGet("{id}/stock")]
	public ActionResult<StockDto[]> Get(int id) {
		Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
		if (product == null) return NotFound();

		List<StockDto> stock = _db.StockAdjustment.Where(u => u.Product_Id == id).Select(s => new StockDto {
			Id = s.Id,
			Change = s.Change,
			Reason = s.Reason,
			Product_Id = s.Product_Id,
			Related_Order_Id = s.Related_Order_Id ?? null,
			CreatedAt = s.CreatedAt
		}).ToList();

		return Ok(stock);
	}

	[HttpGet("{id}/stock/count")]
	public ActionResult<int> GetCount(int id) {
		Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
		if (product == null) return NotFound();

		return Ok(product.Quantity);
	}

	[HttpPost]
	public ActionResult<Product> Create(CreateProductDto productDto) {
		Product newProduct = new Product {
			Name = productDto.Name,
			Description = productDto.Description ?? "",
			Price = productDto.Price,
			Quantity = productDto.Quantity,
			IsActive = productDto.IsActive ?? true,
		};

		_db.Products.Add(newProduct);
		_db.SaveChanges();
		return CreatedAtAction(nameof(Show), new { id = newProduct.Id }, newProduct);
	}

	[HttpPut("{id}")]
	public ActionResult<Product> Update(int id, ProductDto productDto) {
		Product? productFromDb = _db.Products.FirstOrDefault(u => id == u.Id);
		if (productFromDb == null) return NotFound();

		productFromDb.Name = productDto.Name ?? productFromDb.Name;
		productFromDb.Description = productDto.Description ?? productFromDb.Description;
		productFromDb.Storage_Note = productDto.Storage_Note ?? productFromDb.Storage_Note;
		productFromDb.Price = productDto.Price ?? productFromDb.Price;
		productFromDb.Quantity = productDto.Quantity ?? productFromDb.Quantity;
		productFromDb.IsActive = productDto.IsActive ?? productFromDb.IsActive;

		_db.SaveChanges();
		return Ok(productFromDb);
	}

	[HttpPost("'{id}/stock'")]
	public IActionResult Set(int id, setStockDto dto) {
		Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
		if (product == null) return NotFound();

		if (dto.Quantity < 0)
			return BadRequest("Invalid stock adjustment can't get product quantity less than zero");

		StockAdjustment adjustment = new StockAdjustment {
			Change = dto.Quantity - product.Quantity,
			Reason = dto.Reason ?? "",
			Product_Id = product.Id
		};

		product.Quantity = dto.Quantity;

		_db.StockAdjustment.Add(adjustment);
		_db.SaveChanges();

		return Ok();
	}

	[HttpDelete("{id}")]
	public ActionResult Delete(int id) {
		Product? product = _db.Products.FirstOrDefault(u => id == u.Id);
		if (product == null) return NotFound();

		_db.Products.Remove(product);
		_db.SaveChanges();
		return NoContent();
	}
}

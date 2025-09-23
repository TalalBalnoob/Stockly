using Microsoft.AspNetCore.Mvc;

using Stockly.DTOs;
using Stockly.Models;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller {
	private readonly AppDbContext _db;

	public ProductController(AppDbContext db) {
		_db = db;
	}

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

	[HttpPost]
	public ActionResult<Product> Create(Product product) {
		Product newProduct = new Product {
			Name = product.Name,
			Description = product.Description ?? "",
			Price = product.Price,
			Quantity = product.Quantity,
			IsActive = product.IsActive != null ? product.IsActive : true,
		};

		_db.Products.Add(newProduct);
		_db.SaveChanges();
		return CreatedAtAction(nameof(Show), new { id = newProduct.Id }, newProduct);
	}

	[HttpPut("{id}")]
	public ActionResult<Product> Update(int id, ProductDto product) {
		Product productFromDb = _db.Products.FirstOrDefault(u => id == u.Id);
		if (productFromDb == null) return NotFound();

		productFromDb.Name = product.Name ?? productFromDb.Name;
		productFromDb.Description = product.Description ?? productFromDb.Description;
		productFromDb.Price = product.Price ?? productFromDb.Price;
		productFromDb.Quantity = product.Quantity ?? productFromDb.Quantity;
		// productFromDb.ImageUrl = product.ImageUrl;
		productFromDb.IsActive = product.IsActive ?? productFromDb.IsActive;

		_db.SaveChanges();
		return Ok(productFromDb);
	}

	[HttpDelete("{id}")]
	public ActionResult Delete(int id) {
		var product = _db.Products.FirstOrDefault(u => id == u.Id);
		if (product == null) return NotFound();
		_db.Products.Remove(product);
		_db.SaveChanges();
		return NoContent();
	}
}

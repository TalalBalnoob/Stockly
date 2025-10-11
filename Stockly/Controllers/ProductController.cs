using Microsoft.AspNetCore.Mvc;

using Stockly.DTOs;
using Stockly.Models;
using Stockly.Services;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(AppDbContext _db) : Controller
{
	public async Task<PagedResult<Product>> GetProductsAsync(PaginationParams paginationParams, string? search = null)
	{
		var query = _db.Products.AsQueryable();

		var totalCount = query.Count();

		var items = query
			.OrderBy(p => p.Id) // always order for consistent paging
			.Where(p => string.IsNullOrEmpty(search) ||
				p.Name.ToLower().Contains(search.ToLower()) ||
				p.Price.ToString().Contains(search.ToLower()) ||
				p.Quantity.ToString().Contains(search.ToLower()) ||
				p.Storage_Note.ToLower().Contains(search.ToLower())
			)
			.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
			.Take(paginationParams.PageSize)
			.ToList();

		return new PagedResult<Product>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
	}

	[HttpGet]
	public ActionResult<Product[]> Index([FromQuery] PaginationParams paginationParams, [FromQuery] bool? nonDisabled, [FromQuery] string? search = null)
	{
		var products = GetProductsAsync(paginationParams, search).Result;
		products.Items = nonDisabled == true ? products.Items.Where(p => p.IsActive == true).ToArray() : products.Items;
		return Ok(products);
	}

	[HttpGet("{id}")]
	public ActionResult<Product> Show(int id)
	{
		var product = _db.Products.FirstOrDefault(u => id == u.Id);
		if (product == null) return NotFound();

		return Ok(product);
	}

	[HttpGet("{id}/stock")]
	public ActionResult<StockDto[]> Get(int id)
	{
		Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
		if (product == null) return NotFound();

		List<StockDto> stock = _db.StockAdjustment.Where(u => u.Product_Id == id).Select(s => new StockDto
		{
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
	public ActionResult<int> GetCount(int id)
	{
		Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
		if (product == null) return NotFound();

		return Ok(product.Quantity);
	}

	[HttpPost]
	public ActionResult<Product> Create(CreateProductDto productDto)
	{
		Product newProduct = new Product
		{
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
	public ActionResult<Product> Update(int id, ProductDto productDto)
	{
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
	public IActionResult Set(int id, setStockDto dto)
	{
		Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
		if (product == null) return NotFound();

		if (dto.Quantity < 0)
			return BadRequest("Invalid stock adjustment can't get product quantity less than zero");

		StockAdjustment adjustment = new StockAdjustment
		{
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
	public ActionResult Delete(int id)
	{
		Product? product = _db.Products.FirstOrDefault(u => id == u.Id);
		if (product == null) return NotFound();

		_db.Products.Remove(product);
		_db.SaveChanges();
		return NoContent();
	}
}

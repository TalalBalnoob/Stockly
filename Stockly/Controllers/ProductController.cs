using Microsoft.AspNetCore.Mvc;
using Stockly.DTOs;
using Stockly.Models;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller{
    private readonly AppDbContext _db;

    public ProductController(AppDbContext db){
        _db = db;
    }

    [HttpGet]
    public ActionResult<Product[]> Index(){
        var products = _db.Products.ToList();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> Show(int id){
        var product = _db.Products.FirstOrDefault(u => id == u.Id);
        if (product == null) return NotFound();
        
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(CreateProductDto product){
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult<Product> Update(int id, Product product){
        return Ok(_db.Products.FirstOrDefault(u => id == u.Id));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id){
        var product = _db.Products.FirstOrDefault(u => id == u.Id);
        if (product == null) return NotFound();
        _db.Products.Remove(product);
        _db.SaveChanges();
        return NoContent();
    }
}
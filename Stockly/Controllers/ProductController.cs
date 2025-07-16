using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<Product> Create(Product product){
        _db.Products.Add(product);
        _db.SaveChanges();
        return CreatedAtAction(nameof(Show), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public ActionResult<Product> Update(int id, Product product){
        var productFormDb = _db.Products.FirstOrDefault(u => id == u.Id);
        if (productFormDb == null) return NotFound();
        
        productFormDb.Name = product.Name?? productFormDb.Name;
        productFormDb.Description = product.Description ?? productFormDb.Description;
        productFormDb.Price = product.Price ?? productFormDb.Price;
        productFormDb.Quantity = product.Quantity ?? productFormDb.Quantity;
        // todo: work on the image process 
        // productFormDb.ImageUrl = product.ImageUrl ?? productFormDb.ImageUrl;
        
        _db.SaveChanges();
        return Ok(product);
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
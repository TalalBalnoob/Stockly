using Microsoft.AspNetCore.Mvc;
using Stockly.DTOs;
using Stockly.Models;

namespace Stockly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController: Controller{
   private readonly AppDbContext _db;

   public StockController(AppDbContext db){
      _db = db;
   }

   [HttpGet]
   public ActionResult<StockAdjustment[]> Get(){
      var stock = _db.StockAdjustment
         .Select(s => new{
         s.Id,
         s.Change,
         s.Reason,
         s.Product_Id,
         s.CreatedAt
      }).ToList();
      
      return Ok(stock);
   }

   [HttpGet("{id}")]
   public ActionResult<StockAdjustment[]> Get(int id){
      Product product = _db.Products.FirstOrDefault(u => u.Id == id);
      if (product == null) return NotFound();
      
      List<StockAdjustment> stock = _db.StockAdjustment.Where(u => u.Product_Id == id).ToList();
      return Ok(stock);
   }

   [HttpPost("set/{id}")]
   public IActionResult Set(int id, setStockDto dto){
      Product product = _db.Products.FirstOrDefault(u => u.Id == id);
      if (product == null) return NotFound();

      if (product.Quantity + dto.Quantity < 0) 
         return BadRequest("Invalid stock adjustment can't get product quantity less than zero");

      StockAdjustment adjustment = new StockAdjustment{
         Change = dto.Quantity,
         Reason = dto.Reason ?? "",
         Product_Id = product.Id
      };
      
      product.Quantity += dto.Quantity;

      _db.StockAdjustment.Add(adjustment);
      _db.SaveChanges();

      return Ok();
   }

   [HttpGet("count/{id}")]
   public ActionResult<int> GetCount(int id){
      Product product = _db.Products.FirstOrDefault(u => u.Id == id);
      if (product == null) return NotFound();
      
      return Ok(product.Quantity);
   }
}
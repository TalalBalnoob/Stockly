using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class StockAdjustment{
    [Key] public int Id { get; set; }
    [Required] public int Product_Id { get; set; }
    [Required] public int Change { get; set; }
    public string? Reason {get; set;}
    
    [ForeignKey("Product_Id")]
    public Product? Product {get; set;}
    
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    public StockAdjustment(){
        CreatedAt = DateTime.UtcNow;
    }
}
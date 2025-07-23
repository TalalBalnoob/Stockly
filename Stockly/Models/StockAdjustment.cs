using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class StockAdjustment{
    [Key] public string Id { get; set; }
    [Required] public int Variant_Id { get; set; }
    [Required] public int Change { get; set; }
    public string? Reason {get; set;}
    
    [ForeignKey("Variant_Id")]
    public Variant? Variant {get; set;}
    
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    public StockAdjustment(){
        CreatedAt = DateTime.UtcNow;
    }
}
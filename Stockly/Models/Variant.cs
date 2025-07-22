using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class Variant{
    [Key] public int Id { get; set; }
    [Required]public int ProductId { get; set; }
    [Required]public int Quantity { get; set; }
    public decimal? Price_override { get; set; } // optional: overrides base_price
    [Required] public string Internal_code { get; set; } // optional auto-gen (e.g. 'CND-X7F3')
    
    [Column(TypeName = "timestamp")]
    [Required]public DateTime CreatedAt { get; set; }
    
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    
    public Variant()
    {
        CreatedAt = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class Order{
    [Key] public int Id { get; set; }
    public string? Customer_Name { get; set; }
    public string? Customer_Contact { get; set; }
    [Required] public string Status { get; set; }
    [Required] public decimal Totel_amount { get; set; }
    
    // public string PaymentMethod { get; set; } // e.g. "Cash", "Mada"
    
    // public string? PaymentNotes { get; set; }
    
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    public Order(){
        CreatedAt = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Stockly.Models;

[ValidateNever]
public class VariantAttribute{
    [Key] 
    public int Id { get; set; }
    [Required] public int VariantId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Value { get; set; }
    
    [ForeignKey("VariantId")] public virtual Variant Variant { get; set; }
    
}
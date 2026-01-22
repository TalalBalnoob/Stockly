using Stockly.Domain.Entity;

namespace Stockly.Application.DTOs;

public class NewCreatedProductDto {
	public Stock Stock { get; set; }
	public Product Product { get; set; }
}
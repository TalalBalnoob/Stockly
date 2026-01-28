using Stockly.Application.DTOs;
using Stockly.Domain.Enums;

namespace Stockly.Application.UseCases.UpdateOrder;

public class UpdateOrderDto {
	public Guid Id { get; set; }
	public OrderStatus? Status { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Note { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public string? CustomerAddress { get; set; }
	public PaymentStatus? PaymentStatus { get; set; }
	public string? PaymentDetails { get; set; }

	public UpdateOrderItemDto[] OrderItems { get; set; }
}
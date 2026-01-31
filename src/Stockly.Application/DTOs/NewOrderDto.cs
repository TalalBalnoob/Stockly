using Stockly.Domain.Enums;

namespace Stockly.Application.DTOs;

public class NewOrderDto {
	public OrderStatus Status { get; set; } = OrderStatus.Processing;
	public decimal TotalPrice { get; set; }
	public string? Note { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public string? CustomerAddress { get; set; }
	public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
	public string? PaymentDetails { get; set; }

	public List<NewOrderItemDto> OrderItems { get; set; } = new List<NewOrderItemDto>();
}
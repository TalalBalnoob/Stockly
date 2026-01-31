using System.ComponentModel.DataAnnotations;
using Stockly.Domain.Enums;

namespace Stockly.Domain.Entity;

public class Order {
	[Key] public Guid Id { get; set; }
	public OrderStatus Status { get; set; }
	public decimal TotalPrice { get; set; }
	public string? Note { get; set; }
	public string? CustomerName { get; set; }
	public string? CustomerContact { get; set; }
	public string? CustomerAddress { get; set; }
	public PaymentStatus PaymentStatus { get; set; }
	public string? PaymentDetails { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


	public void ChangePaymentStatus(PaymentStatus newStatus) {
		PaymentStatus = newStatus;
	}

	public void ChangeStatus(OrderStatus newStatus) {
		if (!IsValidTransition(Status, newStatus))
			throw new InvalidOperationException(
				$"Cannot change status from {Status} to {newStatus}");

		Status = newStatus;
	}

	private static bool IsValidTransition(
		OrderStatus current,
		OrderStatus next) {
		return (current, next) switch {
			(OrderStatus.Processing, OrderStatus.Shipped) => true,
			(OrderStatus.Processing, OrderStatus.Cancelled) => true,
			(OrderStatus.Shipped, OrderStatus.Delivered) => true,
			(OrderStatus.Delivered, OrderStatus.Returned) => true,
			_ => false
		};
	}
}
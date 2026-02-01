using Stockly.Domain.Entity;
using Stockly.Domain.Enums;

namespace Stockly.Application.Interfaces.Services;

public interface IOrderService
{
    public Task<Order> GetById(Guid id);

    public Task<List<Order>> GetAll();

    public Task<Order> SetOrderStatus(Guid orderId, OrderStatus newStatus);

    public Task<Order> SetPaymentStatus(Guid orderId, PaymentStatus newStatus);
}


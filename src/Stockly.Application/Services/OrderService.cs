using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class OrderService(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo) : IOrderService {
}
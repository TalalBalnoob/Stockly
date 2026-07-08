using Stockly.Application.DTOs.Orders;
using Stockly.Domain.Enums;

namespace Stockly.Application.Interfaces.Repositories;

public interface IOrdersRepo
{
    // Queries
    Task<IEnumerable<OrderResponseDto>> GetAllAsync();
    Task<OrderResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderResponseDto>> GetByCustomerNameAsync(string customerName);
    Task<IEnumerable<OrderResponseDto>> GetByStatusAsync(Order_status status);
    Task<IEnumerable<OrderResponseDto>> GetByDateRangeAsync(DateTime start, DateTime end);

    // Commands
    Task<OrderResponseDto> AddAsync(CreateOrderDto dto);
    Task<OrderResponseDto> UpdateAsync(Guid id, UpdateOrderDto dto);
    Task DeleteAsync(Guid id);

    // Checks
    Task<bool> ExistsAsync(Guid id);
}

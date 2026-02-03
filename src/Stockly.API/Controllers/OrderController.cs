using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stockly.API.ApiDtos.OrderDtos;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Application.UseCases.UpdateOrder;
using Stockly.Domain.Entity;
using Stockly.Domain.Enums;

namespace Stockly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(
	IOrderService orderService,
	IDeleteOrderUseCase deleteOrder,
	IUpdateOrderUseCase updateOrder,
	ICreateNewOrderUseCase createNewOrderUseCase,
	ICancelOrderUseCase cancelOrderUseCase,
	IReturnOrderUseCase returnOrderUseCase
) : ControllerBase {
	[HttpGet]
	public IActionResult GetAll() {
		return Ok(orderService.GetAll());
	}

	[HttpGet("{id}")]
	public IActionResult GetOne(string id) {
		try {
			return Ok(orderService.GetById(Guid.Parse(id)));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> Create(NewOrderDto orderDto) {
		try {
			return Ok(await createNewOrderUseCase.Execute(orderDto));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateOrderDto orderDto) {
		try {
			return Ok(await updateOrder.Execute(orderDto));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("{id}/ship")]
	public async Task<IActionResult> Ship(string id) {
		try {
			return Ok(await orderService.SetOrderStatus(Guid.Parse(id), OrderStatus.Shipped));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("{id}/delivered")]
	public async Task<IActionResult> Delivered(string id) {
		try {
			return Ok(await orderService.SetOrderStatus(Guid.Parse(id), OrderStatus.Delivered));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("{id}/cancel")]
	public async Task<IActionResult> Cancel(string id, CancelOrderDto cancelOrderDto) {
		try {
			return Ok(await cancelOrderUseCase.Execute(Guid.Parse(id), cancelOrderDto));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("{id}/return")]
	public async Task<IActionResult> Return(string id, CancelOrderDto cancelOrderDto) {
		try {
			return Ok(await returnOrderUseCase.Execute(Guid.Parse(id), cancelOrderDto));
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string id) {
		try {
			await deleteOrder.Execute(Guid.Parse(id));
			return NoContent();
		}
		catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}
}
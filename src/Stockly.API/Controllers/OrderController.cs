using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Application.UseCases.UpdateOrder;
using Stockly.Domain.Entity;

namespace Stockly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(
	IOrderService orderService,
	ICreateNewOrderUseCase createNewOrderUseCase,
	IDeleteOrderUseCase deleteOrder,
	IUpdateOrderUseCase updateOrder
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
	public async Task<IActionResult> Post(NewOrderDto orderDto) {
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

	[HttpDelete]
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
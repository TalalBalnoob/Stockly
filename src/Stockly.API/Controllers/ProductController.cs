using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Stockly.Domain.Entity;

namespace Stockly.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetOne(Guid id)
    {
        return this.Ok();
    }

    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? lessThen,
        [FromQuery] int? moreThen,
        [FromQuery] bool? outOfStock
    )
    {
        return this.Ok(new
        {
            LessThen = lessThen,
            MoreThen = moreThen,
            OutOfStock = outOfStock
        });
    }
}

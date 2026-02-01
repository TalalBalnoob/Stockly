using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Domain.Entity;

namespace Stockly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(
    IProductService productService,
    ICreateProductWithStockUseCase productWithStockUseCase,
    IDeleteProductAndStock deleteProduct
) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(productService.GetAllProductsWithStocks());
    }

    [HttpGet("{id}")]
    public IActionResult GetOne(string id)
    {
        try
        {
            return Ok(productService.GetProductWithStocksById(Guid.Parse(id)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(NewProductDto productDto)
    {
        try
        {
            return Ok(await productWithStockUseCase.Execute(productDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(Product product)
    {
        try
        {
            return Ok(await productService.UpdateProduct(product));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await deleteProduct.Execute(Guid.Parse(id));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

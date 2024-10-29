using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Attributes;
using ProductsAPI.Models;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("/api/products")]
[Consumes("application/json")]
[Produces("application/json")]
[Tracker]
[Auth]
public class ProductController(ProductServices productServices) : ControllerBase
{
    [HttpGet("{id}", Name = "GetProducts")]
    public async Task<IActionResult> Get(string id)
    {
        var product = await productServices.Get(id);
        if (product == null) return NotFound("This product was not found");
        return Ok(product);
    }

    [HttpPost("add", Name = "AddProduct")]
    public IActionResult AddProd([FromBody] Product p)
    {
        try
        {
            var prod = productServices.AddProd(p);
            return Ok(prod);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet(Name = "GetAllProducts")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await productServices.GetAll();
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}", Name = "UpdateProduct")]
    public async Task<IActionResult> UpdateProd([FromBody] Product p, [FromRoute] string id)
    {
        try
        {
            var product = await productServices.UpdateProd(p, id);
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    public async Task<IActionResult> DeleteProd([FromRoute] string id)
    {
        try
        {
            var deleteProd = await productServices.DeleteProd(id);
            if (deleteProd) return Ok("Product was deleted");
            return NotFound("This product was not found");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
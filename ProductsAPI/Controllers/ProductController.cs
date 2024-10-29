using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Attributes;
using ProductsAPI.Data;
using ProductsAPI.Filters;
using ProductsAPI.Models;
using System.Reflection;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers
{
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
            IProduct? product = await productServices.Get(id);
            if (product == null) return NotFound("This product was not found");
            return Ok(product);
        }

        [HttpPost("add", Name = "AddProduct")]
        public IActionResult AddProd([FromBody] IProduct p)
        {
            try
            {
                string prod = productServices.AddProd(p);
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
                List<IProduct> products = await productServices.GetAll();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProd([FromBody] IProduct p, [FromRoute] string id)
        {
            try
            {
            IProduct product = await productServices.UpdateProd(p, id);
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
                bool deleteProd = await productServices.DeleteProd(id);
                if (deleteProd) return Ok("Product was deleted");
                return NotFound("This product was not found");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

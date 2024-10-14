using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Attributes;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/products")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProductService : ControllerBase
    {
        public static List<Product> ProductsList = new List<Product>();

        [HttpGet("{id}",Name = "GetProducts")]
        [Tracker("/api/products/{id}")]
        public IActionResult Get(Guid id)
        {
            var prod = ProductsList.FirstOrDefault(p => p.Id.Equals(id));
            if (prod == null)
            {
               NotFound();
            }
            return Ok(prod);
        }

        [HttpPost("add", Name = "AddProduct")]
        [Tracker("/api/products/add")]
        public IActionResult AddProd([FromBody] Product p)
        {
            ProductsList.Add(p);
            return Ok(p.Id);
        }

        // GET: ProductService/Details/5
        
    }
}

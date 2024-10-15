using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Attributes;
using ProductsAPI.Filters;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("/api/products")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Auth]
    [Tracker]
    public class ProductService : ControllerBase
    {
        public static List<Product> ProductsList = new List<Product>();

        [HttpGet("{id}",Name = "GetProducts")]
        
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
        
        public IActionResult AddProd([FromBody] Product p)
        {
            ProductsList.Add(p);
            return Ok(p.Id);
        }

        [HttpGet(Name = "GetAllProducts")]
        public IActionResult GetAll()
        {
            List<Product> retProds = new List<Product>();
            foreach (var p in ProductsList)
            {
                retProds.Add(p);
            }
            return Ok(retProds);
        }

        // GET: ProductService/Details/5
        
    }
}

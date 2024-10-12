using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public Product Get(Guid id)
        {
           var prod = ProductsList.FirstOrDefault(p => p.GetId() == id);
            if (prod == null)
            {
               NotFound();
            }
            return prod;
        }

        [HttpPost("add", Name = "AddProduct")]
        public Boolean AddProd([FromBody] Product p)
        {
            ProductsList.Add(p);
            return true;
        }

        // GET: ProductService/Details/5
        
    }
}

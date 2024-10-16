using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Attributes;
using ProductsAPI.Data;
using ProductsAPI.Filters;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("/api/products")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Tracker]
    [Auth]
    public class ProductService : ControllerBase
    {
        //public static List<Product> ProductsList = new List<Product>();
        private readonly ApiDbContext _context;

        public ProductService(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}",Name = "GetProducts")]
        public async Task<IActionResult> Get(string id)
        {
            Product p = await _context.Products.FindAsync(id);

            if (p == null)
            {
                return NotFound($"This product with {id} ID is not on DB");
            }
            return Ok(p);
        }

        [HttpPost("add", Name = "AddProduct")]
        public IActionResult AddProd([FromBody] Product p)
        {
            _context.Products.Add(p);
            try
            {
                _context.SaveChanges();
                return Ok(p.Id);
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
                await _context.Products.ToListAsync<Product>();
                return Ok(_context.Products);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: ProductService/Details/5
        
    }
}

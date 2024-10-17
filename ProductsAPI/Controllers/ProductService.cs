using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Attributes;
using ProductsAPI.Data;
using ProductsAPI.Filters;
using ProductsAPI.Models;
using System.Reflection;

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

        [HttpGet("{id}", Name = "GetProducts")]
        public async Task<IActionResult> Get(string id)
        {
            Product? p = await _context.Products.FindAsync(id);

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
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProd([FromBody] Product p, [FromRoute] string id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"This product with {p.Id} ID is not on DB");
            }

            PropertyInfo[] properties = typeof(Product).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                if (!propertyName.Equals("Id"))
                {
                    product.GetType().GetProperty(propertyName)!.SetValue(product, p.GetType().GetProperty(propertyName)!.GetValue(p));
                }
            }
            _context.Products.Update(product);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

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
            // Product? p = ProductsList.FirstOrDefault(p => p.Id == id);
            Product? p = await this._context.Products.FindAsync(id);

            if (p == null)
            {
                // Return a response with code 404 Not Found
                return NotFound($"This product with {id} ID is not on DB");
            }
            // Return a response with code 200 OK
            return Ok(p);
        }

        [HttpPost("add", Name = "AddProduct")]
        public IActionResult AddProd([FromBody] Product p)
        {
            // Add the product to the list (memory)
            this._context.Products.Add(p);
            try
            {
                // Save changes in the context (DB)
                this._context.SaveChanges();
                return Ok(p.Id);
            }
            catch (Exception e)
            {
                // Return a response with code 400 Bad Request
                return BadRequest(e.Message);
            }
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // List all Products
                await this._context.Products.ToListAsync<Product>();
                // Return a response with code 200 OK as a JSON as it says on Produces
                return Ok(this._context.Products);
            }
            catch (Exception e)
            {
                // Return a response with code 400 Bad Request
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProd([FromBody] Product p, [FromRoute] string id)
        {
            // Product? product = ProductsList.FirstOrDefault(p => p.Id == id);
            Product? product = await this._context.Products.FindAsync(id);
            if (product == null)
            {
                // Return a response with code 404 Not Found
                return NotFound($"This product with {p.Id} ID is not on DB");
            }
            // We get all properties from the Product class
            PropertyInfo[] properties = typeof(Product).GetProperties();
            foreach (var property in properties)
            {
                // We get the name of the property
                string propertyName = property.Name;
                // If it isn't the ID property, we do the change, if not, we skip it
                if (!propertyName.Equals("Id"))
                {
                    // We get the property from the product of DB and we set the value of the one that we sent from Body
                    product.GetType().GetProperty(propertyName)!.SetValue(product, p.GetType().GetProperty(propertyName)!.GetValue(p));
                }
            }
            // We update the product that we wanted to change
            this._context.Products.Update(product);
            try
            {
                // We save the changes
                await this._context.SaveChangesAsync();
                // Return a response with code 200 OK
                return Ok(product);
            }
            catch (Exception e)
            {
                // Return a response with code 400 Bad Request
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProd([FromRoute] string id)
        {
            // Product? p = ProductsList.FirstOrDefault(p => p.Id == id);
            Product? p = await this._context.Products.FindAsync(id);
            if (p == null)
            {

                // Return a response with code 404 Not Found
                return NotFound($"This product with {id} ID is not on DB");
            }
            // We remove the product from the list
            this._context.Products.Remove(p);
            try
            {
                // We save the changes
                await this._context.SaveChangesAsync();
                // Return a response with code 200 OK
                return Ok("Product deleted");
            }
            catch (Exception e)
            {
                // Return a response with code 400 Bad Request
                return BadRequest(e.Message);
            }
        }
    }
}

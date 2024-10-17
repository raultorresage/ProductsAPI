using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Attributes;
using ProductsAPI.Data;
using ProductsAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/bills")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Tracker]
    [Auth]
    public class BillService : ControllerBase
    {
        private readonly ApiDbContext _context;

        public BillService(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{jwt}", Name = "GetAllBills")]
        public async Task<IActionResult> GetAll([FromRoute] string jwt)
        {
            // We decode the JWT to get the user ID
            string? userId = Jwt.GetUserIdFromToken(jwt);
            if (userId == null)
            {
                return BadRequest("Invalid token");
            }

            List<Bill> bills = await this._context.Bills.Where(b => b.UserId == userId).ToListAsync();
            if (bills.Count == 0)
            {
                return NotFound("No bills found");
            }
            return Ok(bills);
        }

        [HttpPost("{jwt}/add", Name = "AddBill")]
        public async Task<IActionResult> AddBill([FromBody] List<Product> products, [FromRoute] string jwt)
        {
            string? userId = Jwt.GetUserIdFromToken(jwt);
            if (userId == null)
            {
                return BadRequest("Invalid token");
            }
            Bill b = new Bill(userId);
            b.BillProducts = products;
            try
            {
                await this._context.Bills.AddAsync(b);
                try
                {
                    await this._context.SaveChangesAsync();
                    return Ok(b.Id);
                } catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}

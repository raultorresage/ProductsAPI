using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Attributes;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/bills")]
[Consumes("application/json")]
[Produces("application/json")]
[Tracker]
[Auth]
public class BillController(BillServices billServices) : ControllerBase
{
    [HttpGet("{jwt}", Name = "GetAllBills")]
    public async Task<IActionResult> GetAll([FromRoute] string jwt)
    {
        var bills = await billServices.GetBills(jwt);
        if (bills == null) return NotFound();
        return Ok(bills);
    }

    [HttpPost("{jwt}/add", Name = "AddBill")]
    public async Task<IActionResult> AddBill([FromBody] List<string> productsIds, [FromRoute] string jwt)
    {
        try
        {
            var billId = await billServices.AddBill(productsIds, jwt);
            return Ok(billId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{jwt}/{billId}")]
    public async Task<IActionResult> DeleteBill([FromRoute] string jwt, [FromRoute] string billId)
    {
        try
        {
            var deletedBillId = await billServices.DeleteBill(jwt, billId);
            return Ok(deletedBillId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
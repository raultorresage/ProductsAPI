using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Services;

public class BillServices(ApiDbContext context)
{
    public async Task<List<IBill>?> GetBills(string jwt)
    {
        // We decode the JWT to get the user ID
        string? userId = Jwt.GetUserIdFromToken(jwt);
        if (userId == null)
        {
            throw new Exception("Invalid JWT");
        }

        List<IBill> bills = await context.Bills.Where(b => b.UserId == userId).ToListAsync();
        if (bills.Count == 0)
        {
            return null;
        }
        return bills;
    }

    public async Task<string> AddBill(List<string> productsIds, string jwt)
    {
        string? userId = Jwt.GetUserIdFromToken(jwt);
        if (userId == null)
        {
            throw new Exception("Invalid JWT");
        }
        IBill b = new Bill(userId);
        b.BillProductsIds = productsIds;
        try
        {
            await context.Bills.AddAsync(b);
            try
            {
                await context.SaveChangesAsync();
                return b.Id;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<string> DeleteBill(string jwt, string billId)
    {
        string? userId = Jwt.GetUserIdFromToken(jwt);
        if (userId == null)
        {
            throw new Exception("Invalid JWT");
        }

        IBill? b = await context.Bills.FindAsync(billId);
        if (b == null)
        {
            throw new Exception($"This bill was not founded on our system ({billId})");
        }
        context.Bills.Remove(b);
        try
        {
            await context.SaveChangesAsync();
            return $"Bill {billId} has been removed";
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
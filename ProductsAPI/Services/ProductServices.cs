using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Services;

public class ProductServices(ApiDbContext context)
{
    public async Task<IProduct?> Get(string id)
    {
        // Product? p = ProductsList.FirstOrDefault(p => p.Id == id);
        IProduct? p = await context.Products.FindAsync(id);
        return p;
    }

    public string AddProd(IProduct p)
    {
        // Add the product to the list (memory)
        context.Products.Add(p);
        try
        {
            // Save changes in the context (DB)
            context.SaveChanges();
            return p.Id;
        }
        catch (Exception e)
        {
            // Return a response with code 400 Bad Request
            throw new Exception(e.Message);
        }
    }

    public async Task<List<IProduct>> GetAll()
    {
        try
        {
            // List all Products
            List<IProduct> listProd = await context.Products.ToListAsync<IProduct>();
            // Return a response with code 200 OK as a JSON as it says on Produces
            return listProd;
        }
        catch (Exception e)
        {
            // Return a response with code 400 Bad Request
            throw new Exception(e.Message);
        }
    }

    public async Task<IProduct> UpdateProd(IProduct p, string id)
    {
        // Product? product = ProductsList.FirstOrDefault(p => p.Id == id);
        IProduct? product = await context.Products.FindAsync(id);
        if (product == null)
        {
            // Return a response with code 404 Not Found
            throw new Exception($"This product with {p.Id} ID is not on DB");
        }
        // We get all properties from the Product class
        PropertyInfo[] properties = typeof(IProduct).GetProperties();
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
        context.Products.Update(product);
        try
        {
            // We save the changes
            await context.SaveChangesAsync();
            // Return a response with code 200 OK
            return product;
        }
        catch (Exception e)
        {
            // Return a response with code 400 Bad Request
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteProd(string id)
    {
        // Product? p = ProductsList.FirstOrDefault(p => p.Id == id);
        IProduct? p = await context.Products.FindAsync(id);
        if (p == null)
        {
            // Return a response with code 404 Not Found
            return false;
        }
        // We remove the product from the list
        context.Products.Remove(p);
        try
        {
            // We save the changes
            await context.SaveChangesAsync();
            // Return a response with code 200 OK
            return true;
        }
        catch (Exception e)
        {
            // Return a response with code 400 Bad Request
            throw new Exception(e.Message);
        }
    }
}
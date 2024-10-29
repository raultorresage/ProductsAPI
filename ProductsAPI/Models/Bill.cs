namespace ProductsAPI.Models;

public interface IBill
{
    public string Id { get; set; }
    public List<string> BillProductsIds { get; set; }
    public string UserId { get; set; }
}

public class Bill(string userId) : IBill
{
    public string Id { get; set; } = Guid.NewGuid().ToString("D");
    public List<string> BillProductsIds { get; set; } = new();

    public string UserId { get; set; } = userId;
}
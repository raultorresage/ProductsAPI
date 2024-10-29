namespace ProductsAPI.Models;

public interface IProduct
{
    string Name { get; set; }
    string Id { get; set; }
    string Description { get; set; }
    string Author { get; set; }
}

public class Product
{
    public Product(string name, string description, string author)
    {
        Id = Guid.NewGuid().ToString("D");
        Name = name;
        Description = description;
        Author = author;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
}
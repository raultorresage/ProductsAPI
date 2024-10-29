namespace ProductsAPI.Models;

public interface IUser
{
    public string Username { get; set; }
    public string Id { get; set; }
    public string Password { get; set; }
}

public class User : IUser
{
    public User(string username, string password)
    {
        Id = Guid.NewGuid().ToString("D");
        Username = username;
        Password = password;
    }

    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
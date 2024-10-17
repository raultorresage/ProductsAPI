namespace ProductsAPI.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public List<Product> BillProducts { get; set; }

        public string UserId { get; set; }

        public Bill(string userId) { 
            Id = Guid.NewGuid().ToString("D");
            BillProducts = new List<Product>();
            UserId = userId;
        }

    }
}

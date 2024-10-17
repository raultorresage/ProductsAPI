namespace ProductsAPI.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public List<Product> BillProducts { get; set; }

        public string UserId { get; set; }

        public Bill(string UserId) { 
            Id = Guid.NewGuid().ToString("D");
            BillProducts = new List<Product>();
        }

    }
}

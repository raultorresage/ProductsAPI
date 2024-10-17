namespace ProductsAPI.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public List<string> BillProductIds { get; set; }

        public string UserId { get; set; }

        public Bill(string userId) { 
            Id = Guid.NewGuid().ToString("D");
            BillProductIds = new List<string>();
            UserId = userId;
        }

    }
}

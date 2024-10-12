namespace ProductsAPI.Models
{
    public class Product
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string Author { get; set; }

        public Product(Guid id, string name, string description, string author)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Author = author;
        }

        public Guid GetId() { return this.Id; }
        public void SetId(Guid id) { this.Id = id; }

        public string GetName() { return this.Name; }
        public void SetName(string name) { this.Name = name; }

        public string GetDescription() 
            { return this.Description; }
        public void SetDescription(string description) { this.Description = description; }

        public string GetAuthor()
        {
            return this.Author;
        }

        public void SetAuthor(string author) { this.Author = author; }

    }
}

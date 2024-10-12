namespace ProductsAPI.Models
{
    public class User
    {

        public User(string username, string password)
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.Password = password;
        }

        private Guid Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }

        public Guid GetId() { return this.Id; }
        public string GetUserName()
        {
            return this.Username;
        }

        public string GetPassword()
        {
            return this.Password;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        public void SetUsername(string username)
        {
            this.Username = username;
        }

        public void SetPassword(string password)
        {
            this.Password = password;
        }

    }
}

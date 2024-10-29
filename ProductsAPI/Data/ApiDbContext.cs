using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Data
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<IProduct> Products { get; set; }
        public DbSet<IUser> Users { get; set; }

        public DbSet<Bill> Bills { get; set; }
    }
}

//Deeper and EntityFramework
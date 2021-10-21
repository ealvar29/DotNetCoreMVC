using Microsoft.EntityFrameworkCore;
using MVCDotNet5.Models;

namespace MVCDotNet5.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories {  get; set; }

        public DbSet<ApplicationType> ApplicationTypes {  get; set; }
    }
}

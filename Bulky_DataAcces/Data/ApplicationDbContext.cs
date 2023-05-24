using Ketab_Models;
using Microsoft.EntityFrameworkCore;

namespace Ketab_DataAcces.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

           

		}
        public DbSet<Category> categories { get; set; }
		public DbSet< Product >  Products { get; set; }

	}
}

using Bulky_WebRazor_Temp.Model;
using Microsoft.EntityFrameworkCore;

namespace Bulky_WebRazor_Temp.Data
{
	public class ApplicationDbcontex :DbContext
	{
        public ApplicationDbcontex( DbContextOptions<ApplicationDbcontex> options) :base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, Name = "action", DesplayOrder = 1 },
				new Category { CategoryId = 2, Name = "sidi", DesplayOrder = 2 },
				new Category { CategoryId = 3, Name = "misic", DesplayOrder = 3 }

				);
		}
		public DbSet<Category> categories { get; set; }
    }
}

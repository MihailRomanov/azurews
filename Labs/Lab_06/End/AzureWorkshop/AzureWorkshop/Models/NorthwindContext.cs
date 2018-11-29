using Microsoft.EntityFrameworkCore;

namespace AzureWorkshop.Models
{
	public class NorthwindContext : DbContext
	{
		public NorthwindContext(DbContextOptions<NorthwindContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
	}
}

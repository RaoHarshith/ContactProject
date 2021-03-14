using Microsoft.EntityFrameworkCore;

namespace ContactApi.Models
{
    public class Context : DbContext
    {
		public Context(DbContextOptions<Context> options)
		: base(options)
		{
		}
		public DbSet<Contact> Contact { get; set; }
	}
}
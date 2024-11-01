using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

public class ReadyProductContext : DbContext
{
	public ReadyProductContext(DbContextOptions<ReadyProductContext> options)
	: base(options)
	{
	}

	public DbSet<ReadyProductcs> Products { get; set; }
}
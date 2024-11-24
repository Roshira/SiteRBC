using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

public class SiteRBCContext : DbContext
{
	public SiteRBCContext(DbContextOptions<SiteRBCContext> options)
	: base(options)
	{
	}

	public DbSet<ReadyProduct> Products { get; set; }

	public DbSet<Users> Users { get; set; }
}
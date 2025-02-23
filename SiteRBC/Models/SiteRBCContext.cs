using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

/**
 * @class SiteRBCContext
 * @brief Database context class for the SiteRBC application.
 * 
 * This class is responsible for managing database connections and providing 
 * access to different entities within the application.
 */
public class SiteRBCContext : DbContext
{
    /**
     * @brief Constructor for SiteRBCContext.
     * @param options Database context options.
     * 
     * Initializes a new instance of the SiteRBCContext with the specified options.
     */
    public SiteRBCContext(DbContextOptions<SiteRBCContext> options)
        : base(options)
    {
    }

    /**
     * @brief Gets or sets the database table for ready products.
     */
    public DbSet<ReadyProduct> Products { get; set; }

    /**
     * @brief Gets or sets the database table for users.
     */
    public virtual DbSet<Users> Users { get; set; }

    /**
     * @brief Gets or sets the database table for user information.
     */
    public DbSet<UsersInfo> UsersInfo { get; set; }
}

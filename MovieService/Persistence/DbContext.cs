public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; }
}
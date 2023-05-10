public interface IDbContext
{
    DbSet<Movie> Movie { get; set; }
    Task<int> SaveChangesAsync();
}
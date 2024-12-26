using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database;

public class AppDbContext : DbContext
{
    public DbSet<DbConfiguration> DbConfiguration { get; set; }
    public DbSet<DbSaveGame> DbSaveGame { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
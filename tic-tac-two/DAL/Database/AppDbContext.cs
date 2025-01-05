using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<DbConfiguration> DbConfiguration { get; set; }
    public DbSet<DbSaveGame> DbSaveGame { get; set; }
}
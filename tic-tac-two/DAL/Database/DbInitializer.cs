using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DAL.Database;

public class DatabaseInitializer
{
    private readonly AppDbContext _dbContext;
    private static readonly string DbFilePath = FileHelper.BasePath + "app.db";
    private readonly string _connectionString = $"Data Source={DbFilePath}";

    public DatabaseInitializer()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite(_connectionString);
        _dbContext = new AppDbContext(optionsBuilder.Options);
    }

    public string GetConnectionString()
    {
        return _connectionString;
    }

    
    public void Initialize()
    {
        try
        {
            _dbContext.Database.Migrate();

            EnsureDefaultConfigurations();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization failed: {ex.Message}");
            throw;
        }
    }

    private void EnsureDefaultConfigurations()
    {
        try
        {
            if (_dbContext.DbConfiguration.Any()) return;

            var defaultConfigs = new List<DbConfiguration>
            {
                new DbConfiguration
                {
                    ConfigurationName = "Classical Tic-Tac-Two",
                    BoardSizeWidth = 5,
                    BoardSizeHeight = 5,
                    GridSizeWidth = 3,
                    GridSizeHeight = 3,
                    WinCondition = 3,
                    MovePieceAfterNMoves = 3,
                    Username = null
                },
                new DbConfiguration
                {
                    ConfigurationName = "Big Board",
                    BoardSizeWidth = 10,
                    BoardSizeHeight = 10,
                    GridSizeWidth = 5,
                    GridSizeHeight = 5,
                    WinCondition = 4,
                    MovePieceAfterNMoves = 5,
                    Username = null
                }
            };

            _dbContext.DbConfiguration.AddRange(defaultConfigs);
            _dbContext.SaveChanges();
            Console.WriteLine("Default configurations seeded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding default configurations: {ex.Message}");
            throw;
        }
    }
    
    public AppDbContext GetDbContext() => _dbContext;
    public void Dispose() => _dbContext.Dispose();
}
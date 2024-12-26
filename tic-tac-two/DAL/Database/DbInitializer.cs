using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DAL.Database;

public class DbInitializer
{
    private readonly AppDbContext _dbContext;
    private readonly string _connectionString = $"Data Source={FileHelper.BasePath + "app.db"}";

    public DbInitializer()
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
        catch (Exception e)
        {
            Console.WriteLine($"Database initialization failed: {e.Message}");
            throw;
        }
    }

    private void EnsureDefaultConfigurations()
    {
        try
        {
            if (_dbContext.DbConfiguration.Any()) return;

            var defaultConfigurations = new List<DbConfiguration>
            {
                new DbConfiguration()
                {
                    ConfigurationName = "Classical",
                    BoardSizeWidth = 5,
                    BoardSizeHeight = 5,
                    GridSizeWidth = 3,
                    GridSizeHeight = 3,
                    WinCondition = 3,
                    MovePieceAfterNMoves = 1
                },

                new DbConfiguration()
                {
                    ConfigurationName = "Big Board",
                    BoardSizeWidth = 10,
                    BoardSizeHeight = 10,
                    GridSizeWidth = 5,
                    GridSizeHeight = 5,
                    WinCondition = 4,
                    MovePieceAfterNMoves = 3
                }
            };

            _dbContext.DbConfiguration.AddRange(defaultConfigurations);
            _dbContext.SaveChanges();
            Console.WriteLine("Default configurations added successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while adding default configurations: {e.Message}");
            throw;
        }
    }

    public AppDbContext GetDbContext()
    {
        return _dbContext;
    }

    public void Dispose() => _dbContext.Dispose();
}
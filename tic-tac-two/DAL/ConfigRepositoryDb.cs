using DAL.Database;
using Domain;
using GameLogic;


namespace DAL;

public class ConfigRepositoryDb : IConfigRepository
{
    private readonly AppDbContext _context;

    public ConfigRepositoryDb(AppDbContext context)
    {
        _context = context;
    }

    public List<string> GetConfigurationNames(string username)
    {
        InitializeDefaultConfigurations(username);
        
        return _context.DbConfiguration
            .Where(c => c.Username == username)
            .Select(c => c.ConfigurationName)
            .ToList();
    }

    public List<GameConfiguration> GetAllConfigurations(string username)
    {
        InitializeDefaultConfigurations(username);
        
        return _context.DbConfiguration
            .Where(c => c.Username == username)
            .ToList()
            .Select(ConfigurationConverter.ToGameConfiguration)
            .ToList();
    }

    public GameConfiguration GetConfiguration(string name, string username)
    {
        var dbConfig = _context.DbConfiguration
            .FirstOrDefault(c => c.ConfigurationName == name && c.Username == username);

        if (dbConfig == null)
        {
            throw new KeyNotFoundException($"Configuration with name '{name}' was not found for user '{username}'.");
        }

        return ConfigurationConverter.ToGameConfiguration(dbConfig);
    }
    
    public void SaveConfiguration(GameConfiguration config, string username)
    {
        var dbConfig = ConfigurationConverter.ToTicTacTwoConfiguration(config);
        dbConfig.Username = username;
        _context.DbConfiguration.Add(dbConfig);

        _context.SaveChanges();
    }

    public void DeleteConfiguration(GameConfiguration config, string username)
    {
        var dbConfig = _context.DbConfiguration
            .FirstOrDefault(c => c.ConfigurationName == config.Name);
        if (dbConfig != null) _context.DbConfiguration.Remove(dbConfig);
        _context.SaveChanges();
    }

    private void InitializeDefaultConfigurations(string username)
    {
        var configs = _context.DbConfiguration
            .Where(c => c.Username == username)
            .ToList()
            .Select(ConfigurationConverter.ToGameConfiguration)
            .ToList();

        if (configs.Count != 0) return;
        var defaultConfigs = new List<DbConfiguration>
        {
            new DbConfiguration
            {
                ConfigurationName = "Classical",
                BoardSizeWidth = 5,
                BoardSizeHeight = 5,
                GridSizeWidth = 3,
                GridSizeHeight = 3,
                WinCondition = 3,
                MovePieceAfterNMoves = 3,
                Username = username
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
                Username = username
            }
        };

        _context.DbConfiguration.AddRange(defaultConfigs);
        _context.SaveChanges();
    }
}
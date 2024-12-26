using DAL.Database;
using Domain;
using GameLogic;


namespace DAL;

public class ConfigRepositoryDb
{
    private readonly AppDbContext _context;

    public ConfigRepositoryDb(AppDbContext context)
    {
        _context = context;
    }

    public List<string> GetConfigurationNames()
    {
        return _context.DbConfiguration.Select(c => c.ConfigurationName).ToList();
    }

    public List<GameConfiguration> GetAllConfigurations()
    {
        return _context.DbConfiguration
            .ToList()
            .Select(ConfigurationConverter.ToGameConfiguration)
            .ToList();
    }

    public GameConfiguration GetConfiguration(string name)
    {
        var dbConfig = _context.DbConfiguration
            .SingleOrDefault(c => c.ConfigurationName == name);
        if (dbConfig == null)
        {
            throw new KeyNotFoundException($"Did not find configuration with name {name}");
        }

        return ConfigurationConverter.ToGameConfiguration(dbConfig);
    }

    public void SaveConfiguration(GameConfiguration configuration)
    {
        var dbConfig = ConfigurationConverter.ToTicTacTwoConfiguration(configuration);
        _context.DbConfiguration.Add(dbConfig);
        _context.SaveChanges();
    }
}
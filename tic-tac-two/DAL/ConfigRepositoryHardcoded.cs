using Domain;
using GameLogic;

namespace DAL;

public class ConfigRepositoryHardcoded : IConfigRepository
{
    private static readonly List<GameConfiguration> GameConfigurations =
    [
        new GameConfiguration()
        {
            Name = "Classical Tic-Tac-Two"
        },

        new GameConfiguration()
        {
            Name = "Big board",
            BoardSizeWidth = 10,
            BoardSizeHeight = 10,
            WinCondition = 4,
            MovePieceAfterNMoves = 3,
        }
    ];

    public List<string> GetConfigurationNames(string username)
    {
        return GameConfigurations
            .Select(config => config.Name)
            .ToList();
    }

    public GameConfiguration GetConfiguration(string configName, string username)
    {
        return GameConfigurations.Single(config => config.Name == configName);
    }

    public List<GameConfiguration> GetAllConfigurations(string username)
    {
        return GameConfigurations;
    }

    public void SaveConfiguration(GameConfiguration config, string username)
    {
        GameConfigurations.Add(config);
    }

    public void DeleteConfiguration(GameConfiguration config, string username)
    {
        GameConfigurations.Remove(config);
    }
    
}
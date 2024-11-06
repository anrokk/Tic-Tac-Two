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

    public List<string> GetConfigurationNames()
    {
        return GameConfigurations
            .Select(config => config.Name)
            .ToList();
    }

    public GameConfiguration GetConfigurationByName(string configName)
    {
        return GameConfigurations.Single(config => config.Name == configName);
    }

    public void SaveConfiguration(GameConfiguration config)
    {
        GameConfigurations.Add(config);
    }
    
}
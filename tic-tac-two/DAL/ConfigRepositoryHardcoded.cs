using GameLogic;

namespace DAL;

public class ConfigRepositoryHardcoded : IConfigRepository
{
    private List<GameConfiguration> _gameConfigurations = new List<GameConfiguration>()
    {
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
        },

    };

    public List<string> GetConfigurationNames()
    {
        return _gameConfigurations
            .Select(config => config.Name)
            .ToList();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        return _gameConfigurations.Single(c => c.Name == name);
    }
    
}
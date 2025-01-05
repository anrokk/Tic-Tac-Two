using DAL.Database;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class GameRepositoryDb : IGameRepository
{
    private readonly AppDbContext _context;
    private string? _currentGameId;

    public GameRepositoryDb(AppDbContext context)
    {
        _context = context;
    }
    
    public void SaveGame(GameState gameState, string username)
    {
        var config = _context.DbConfiguration
            .FirstOrDefault(c => c.ConfigurationName == gameState.GetGameConfigurationName());

        if (config == null)
        {
            throw new ArgumentException($"Configuration with name '{gameState.GetGameConfigurationName()}' not found.");
        }

        var saveGame = _context.DbSaveGame
            .FirstOrDefault(s => s.Id == _currentGameId && s.Username == username);

        if (saveGame == null)
        {
            if (_context.Entry(config).State == EntityState.Detached)
            {
                _context.Attach(config);
            }

            saveGame = new DbSaveGame
            {
                GameName = $"{gameState.GetGameConfigurationName()} {DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}",
                State = gameState.ToString(),
                StateId = gameState.GetGameId(),
                ConfigurationName = config.ConfigurationName,
                Configuration = config,
                Username = username
            };

            _context.DbSaveGame.Add(saveGame);
        }
        else
        {
            saveGame.State = gameState.ToString();
        }

        _context.SaveChanges();
        _currentGameId = saveGame.Id;
    }

    public List<string> GetAllGameNames(string username)
    {
        return _context.DbSaveGame
            .Include(s => s.Configuration)
            .AsNoTracking()
            .Where(s => s.Username == username)
            .Select(s => s.GameName)
            .ToList();
    }

    public List<GameState> GetAllGameStates(string username)
    {
        return _context.DbSaveGame
            .AsNoTracking()
            .Where(s => s.Username == username)
            .Select(s => GameState.FromJson(s.State))
            .ToList();
    }

    public GameState LoadGame(string stateId, string username)
    {
        var saveGame = _context.DbSaveGame
            .FirstOrDefault(s => s.StateId == stateId && s.Username == username);

        if (saveGame == null)
        {
            throw new InvalidOperationException($"No save game found with ID '{stateId}' for user '{username}'.");
        }

        _currentGameId = saveGame.Id;
        return GameState.FromJson(saveGame.State);
    }

    public void DeleteGame(string gameId, string username)
    {
        var saveGame = _context.DbSaveGame
            .FirstOrDefault(s => s.StateId == gameId && s.Username == username);

        if (saveGame != null)
        {
            _context.DbSaveGame.Remove(saveGame);
            _context.SaveChanges();
        }
    }
}
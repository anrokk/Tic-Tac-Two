using System.Text.Json;
using Domain;
using GameLogic;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{

    private string? _currentGameFile;

    public void SaveGame(GameState gameState, string username)
    {
        gameState.Username = username;
        var fileName = FileHelper.BasePath + gameState.GetGameId() + "_" + username + FileHelper.GameExtension;
        File.WriteAllText(fileName, gameState.ToString());
        _currentGameFile = fileName;
    }

    public List<string> GetAllGameNames(string username)
    {
        var gameFiles = Directory.GetFiles(FileHelper.BasePath, $"*_{username}{FileHelper.GameExtension}").ToList();

        return gameFiles.Select(filePath =>
        {
            var gameJsonStr = File.ReadAllText(filePath);
            var game = JsonSerializer.Deserialize<GameState>(gameJsonStr);
            return $"{game?.GetGameConfigurationName()} {game?.GetCreatedAt()}";
        }).ToList();
    }
    
    public List<GameState> GetAllGameStates(string username)
    {
        var gameFiles = Directory.GetFiles(FileHelper.BasePath, $"*_{username}{FileHelper.GameExtension}").ToList();

        return gameFiles
            .Select(file => JsonSerializer.Deserialize<GameState>(File.ReadAllText(file)))
            .Where(game => game != null && game.Username == username)
            .ToList()!;
    }

    public GameState LoadGame(string gameId, string username)
    {
        var filePath = FileHelper.BasePath + gameId + "_" + username + FileHelper.GameExtension;

        if (!File.Exists(filePath))
        {
            throw new InvalidOperationException($"Game with ID {gameId} for user {username} not found.");
        }

        var gameJsonStr = File.ReadAllText(filePath);
        var game = JsonSerializer.Deserialize<GameState>(gameJsonStr);

        _currentGameFile = filePath;
        return game ?? throw new InvalidOperationException("Failed to load game state.");
    }

    public void DeleteGame(string gameId, string username)
    {
        var filePath = FileHelper.BasePath + gameId + "_" + username + FileHelper.GameExtension;

        if (!File.Exists(filePath))
        {
            throw new InvalidOperationException($"Game with ID {gameId} for user {username} not found.");
        }

        File.Delete(filePath);
    }
    
}
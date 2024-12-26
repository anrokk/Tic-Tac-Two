using System.Text.Json;
using Domain;
using GameLogic;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{

    private string? _currentGameFile;
    
    public void SaveGame(string jsonStateString, string gameConfigName)
    {
        var fileName = FileHelper.BasePath +
                       gameConfigName + " " +
                       DateTime.Now.ToString("yyyy-MM-dd--HH--mm--ss") +
                       FileHelper.GameExtension;
        File.WriteAllText(fileName, jsonStateString);
        _currentGameFile = fileName;
    }

    public List<string> GetGameNames()
    {
        var gameFiles = Directory
            .GetFiles(FileHelper.BasePath, 
                "*" + 
                FileHelper.ConfigExtension).ToList();

        return gameFiles.Select(Path.GetFileNameWithoutExtension).Select(Path.GetFileNameWithoutExtension).ToList()!;
    }

    public GameState LoadGame(string name)
    {
        var filePath = FileHelper.BasePath + name + FileHelper.GameExtension;
        var gameJsonStr = File.ReadAllText(FileHelper.BasePath + name + FileHelper.GameExtension);
        var game = JsonSerializer.Deserialize<GameState>(gameJsonStr);

        _currentGameFile = filePath;

        return game ?? throw new InvalidOperationException("Game was not found");
    }
    
}
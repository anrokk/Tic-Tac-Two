namespace Domain;

public class GameState
{
    public string GameId { get; init; } = null!;
    public string GameConfigurationName { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public EGamePiece[][] GameBoard { get; set; } = null!;
    public EGamePiece NextMoveBy { get; set; } = EGamePiece.X;
    public GameConfiguration GameConfiguration { get; init; } = null!;
    public bool IsGameOver { get; set; }
    
    public int GridStartX { get; set; }
    public int GridStartY { get; set; }
    public int GridEndX { get; set; }
    public int GridEndY { get; set; }
    
    public GameState(EGamePiece[][] gameBoard, GameConfiguration gameConfiguration, string? gameId = null) 
    {
        GameBoard = gameBoard;
        GameConfiguration = gameConfiguration;
        GameId = gameId ?? IdGenerator.GenerateId();
        GameConfigurationName = gameConfiguration.Name;
        CreatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
    
    public GameConfiguration GetGameConfiguration() => GameConfiguration;

    public string GetGameConfigurationName() => GameConfigurationName;
    
    public string GetCreatedAt() => CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");

    public string GetGameId() => GameId;

    public static GameState FromJson(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<GameState>(json)!;
    }

}
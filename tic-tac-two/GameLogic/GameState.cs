namespace GameLogic;

public class GameState
{
    public EGamePiece[][] GameBoard { get; set; }
    public EGamePiece NextMoveBy { get; set; } = EGamePiece.X;
    public GameConfiguration GameConfiguration { get; set; }
    public int GridStartX { get; set; }
    public int GridStartY { get; set; }
    public int GridEndX { get; set; }
    public int GridEndY { get; set; }
    
    public GameState(EGamePiece[][] gameBoard, GameConfiguration gameConfiguration)
    {
        GameBoard = gameBoard;
        GameConfiguration = gameConfiguration;
    }

    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
    
}
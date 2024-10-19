namespace GameLogic;

public class GameConfiguration
{
    public string Name { get; set; } = default!;
    
    public int BoardSizeX { get; set; } = 5;
    public int BoardSizeY { get; set; } = 5;

    public int WinCondition { get; set; } = 3;
    
    public int MovePieceAfterNMoves { get; set; } = 0;

}
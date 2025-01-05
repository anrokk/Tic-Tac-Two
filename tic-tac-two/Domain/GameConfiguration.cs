namespace Domain;

public class GameConfiguration
{
    public string Name { get; set; } = null!;
    public int BoardSizeWidth { get; set; } = 5;
    public int BoardSizeHeight { get; set; } = 5;
    public int GridSizeWidth { get; set; } = 3;
    public int GridSizeHeight { get; set; } = 3;
    public int WinCondition { get; set; } = 3;
    public int MovePieceAfterNMoves { get; set; } = 3;
    public string Username { get; set; } = null!;
    public override string ToString() => 
        $"Configuration name {Name}" +
        $"Board {BoardSizeWidth}x{BoardSizeHeight}, "+
        $"Grid {GridSizeWidth}x{GridSizeHeight}, "+
        $"to win: {WinCondition}, " + 
        $"can move piece after {MovePieceAfterNMoves} moves";
    
}
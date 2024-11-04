namespace GameLogic;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    public int BoardSizeWidth { get; set; } = 5;
    public int BoardSizeHeight { get; set; } = 5;
    public int GridSizeWidth { get; set; } = 3;
    public int GridSizeHeight { get; set; } = 3;
    public int WinCondition { get; set; } = 3;
    public int MovePieceAfterNMoves { get; set; } = 0;

    public override string ToString() =>
    $"Board {BoardSizeWidth}x{BoardSizeHeight}, "+
    $"Grid {GridSizeWidth}x{GridSizeHeight}, "+
    "to win: {WinCondition}, " + 
    "can move piece after {MovePieceAfterNMoves} moves";
    
}
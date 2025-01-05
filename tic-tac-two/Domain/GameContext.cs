namespace Domain;

public class GameContext
{
    public bool MovingGrid { get; set; }
    public bool MovingPiece { get; set; }
    public int? SelectedX { get; set; }
    public int? SelectedY { get; set; }
    public string? Direction { get; set; }
}
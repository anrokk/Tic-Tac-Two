using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DbConfiguration
{
    public int Id { get; set; }

    [MaxLength(128)] public string ConfigurationName { get; set; } = null!;

    public int BoardSizeWidth { get; set; }
    public int BoardSizeHeight { get; set; }
    public int GridSizeWidth { get; set; }
    public int GridSizeHeight { get; set; }
    public int WinCondition { get; set; }
    public int MovePieceAfterNMoves { get; set; }


    public ICollection<DbSaveGame>? SaveGames { get; init; }
    
}
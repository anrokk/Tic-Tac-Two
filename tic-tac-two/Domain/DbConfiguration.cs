using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DbConfiguration
{
    public int Id { get; init; }

    [MaxLength(128)]
    public string ConfigurationName { get; init; } = null!;

    public int BoardSizeWidth { get; init; }
    public int BoardSizeHeight { get; init; }

    public int GridSizeWidth { get; init; }
    public int GridSizeHeight { get; init; }

    public int WinCondition { get; init; }

    public int MovePieceAfterNMoves { get; init; }
    public ICollection<DbSaveGame> SaveGames { get; init; } = new List<DbSaveGame>();
    [MaxLength(128)]
    public string? Username { get; set; }
    
    public override string ToString()
    {
        return $"{ConfigurationName}, Width: {BoardSizeWidth}, Height: {BoardSizeHeight}, ID: {Id}";;
    }

}
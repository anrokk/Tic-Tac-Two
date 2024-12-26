using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DbSaveGame
{
    // Primary Key
    public int Id { get; init; }

    [MaxLength(128)] public string GameName { get; init; } = null!;

    [MaxLength(128)]
    public DateTime CreatedAtDateTime { get; set; } = DateTime.UtcNow;

    [MaxLength(10240)]
    public string State { get; set; } = null!;

    [MaxLength(128)]
    public string StateId { get; set; } = null!;
    
    public int ConfigurationId { get; init; }
    
    [MaxLength(128)]
    public string ConfigurationName { get; init; } = null!;
    
    public DbConfiguration? Configuration { get; init; }
}
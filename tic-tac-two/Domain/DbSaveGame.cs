using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DbSaveGame
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    [MaxLength(128)]
    public string GameName { get; init; } = null!;
    
    public DateTime CreatedAtDateTime { get; init; } = DateTime.UtcNow;

    [MaxLength(10240)]
    public string State { get; set; } = null!;

    [MaxLength(128)]
    public string StateId { get; set; } = null!;
    
    public int ConfigurationId { get; init; }
    
    [MaxLength(128)]
    public string ConfigurationName { get; init; } = null!;
    
    public DbConfiguration? Configuration { get; init; }
    [MaxLength(128)]
    public string Username { get; set; } = null!;
}
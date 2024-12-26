using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DbSaveGame
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string CreatedAtDateTime { get; set; } = default!;

    [MaxLength(10240)]
    public string State { get; set; } = null!;
    
    public int ConfigurationId { get; set; }
    
    public DbConfiguration? Configuration { get; set; }
}
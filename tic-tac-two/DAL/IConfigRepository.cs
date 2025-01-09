using Domain;

namespace DAL;

public interface IConfigRepository
{
    List<string> GetConfigurationNames(string username);
    GameConfiguration GetConfiguration(string name, string username);
    public List<GameConfiguration> GetAllConfigurations(string username);
    void SaveConfiguration(GameConfiguration config, string username);
    void DeleteConfiguration(GameConfiguration config, string username);
}
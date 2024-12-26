using Domain;
using GameLogic;

namespace DAL;

public interface IConfigRepository
{
    List<string> GetConfigurationNames();
    GameConfiguration GetConfigurationByName(string name);
    void SaveConfiguration(GameConfiguration config);
}
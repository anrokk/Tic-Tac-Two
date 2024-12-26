using Domain;
using GameLogic;

namespace DAL;

public interface IGameRepository
{
    public void SaveGame(string jsonStateString, string gameConfigName);

    List<string> GetGameNames();

    GameState LoadGame(string configName);
}
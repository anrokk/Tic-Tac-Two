using Domain;
using GameLogic;

namespace DAL;

public interface IGameRepository
{
    void SaveGame(GameState gameState, string username);
    List<string> GetAllGameNames(string username);
    List<GameState> GetAllGameStates(string username);
    GameState LoadGame(string gameId, string username);
    void DeleteGame(string gameId, string username);
}
using DAL;
using GameLogic;
using MenuSystem;

namespace ConsoleApp;

public class GameController(IConfigRepository configRepository, IGameRepository gameRepository, string username)
{
    public string StartNewGame()
    {
        return MainLoop();
    }

    public string LoadSavedGame()
    {
        var chosenGameId = ChooseGameSave();
        var chosenGame = gameRepository.LoadGame(chosenGameId, username);
        var gameInstance = new TicTacTwoBrain(chosenGame);
        
        return MainLoop(gameInstance);
    }

    private string MainLoop(TicTacTwoBrain? gameInstance = null)
    {
        if (gameInstance == null)
        {
            var chosenConfigShortcut = ChooseConfiguration();
            
            if (!int.TryParse(chosenConfigShortcut, out var configNo))
            {
                return chosenConfigShortcut;
            }
            
            var chosenConfig = configRepository.GetConfiguration(
                configRepository.GetConfigurationNames(username)[configNo], username);

            gameInstance = new TicTacTwoBrain(chosenConfig);
        }
        
        var gameType = ChooseGameType();

        switch (gameType)
        {
            case "PvsP":
            {
                GameLoops.PlayerAgainstPlayer(gameInstance, gameRepository, username); 
                break;
            }
            case "PvsAi":
            {
                GameLoops.PlayerAgainstAi(gameInstance, gameRepository, username);
                break;
            }
            default:
            {
                GameLoops.AiAgainstAi(gameInstance, gameRepository, username);
                break;
            }
        }
        return "Exit";
    }

    private string ChooseConfiguration()
    {
        var configMenuItems = new List<MenuItem>();

        for (var i = 0; i < configRepository.GetConfigurationNames(username).Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(new MenuItem()
            {
                Title = configRepository.GetConfigurationNames(username)[i],
                Shortcut = (i+1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
        var configMenu = new Menu(
            "Choose game configuration", 
            configMenuItems, EMenuLevel.Secondary, 
            isCustomMenu: true);

        return configMenu.Run();
    }

    private string ChooseGameSave()
    {
        var allGameStates = gameRepository.GetAllGameStates(username);
        var gameMenuItems = allGameStates.Select((gameState, i) => new MenuItem()
        {
            Title = $"{gameState.GetGameConfigurationName()} {gameState.GetCreatedAt()}",
            Shortcut = (i + 1).ToString(),
            MenuItemAction = () =>
            {
                var gameId = gameState.GetGameId();
                return gameId;
            }
        }).ToList();

        var gameMenu = new Menu("Choose game save file", gameMenuItems, EMenuLevel.Secondary, isCustomMenu: true);
        
        return gameMenu.Run();
    }

    private static string ChooseGameType()
    {
        var typeMenuItems = new List<MenuItem>
        {
            new()
            {
                Title = "Player vs AI",
                Shortcut = "1",
                MenuItemAction = () => "PvsAi"
            },
            new()
            {
                Title = "Player vs Player",
                Shortcut = "2",
                MenuItemAction = () => "PvsP"
            },
            new()
            {
                Title = "AI vs AI",
                Shortcut = "3",
                MenuItemAction = () => "AivsAi"
            }
        };
        
        var typeMenu = new Menu("Choose game type", typeMenuItems, EMenuLevel.Deep, isCustomMenu: true);
        
        return typeMenu.Run();
    }
}
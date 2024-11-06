using DAL;
using GameLogic;
using MenuSystem;
using ConsoleUI;

namespace ConsoleApp;

public class GameController
{

    private static readonly IConfigRepository ConfigRepository = new ConfigRepositoryJson();
    private static readonly IGameRepository GameRepository = new GameRepositoryJson();
    
    public string StartNewGame()
    {
        return MainLoop();
    }

    public string LoadSavedGame()
    {
        var chosenGameShortcut = ChooseSavedGame();

        if (!int.TryParse(chosenGameShortcut, out var gameNo))
        {
            return chosenGameShortcut;
        }
        
        var chosenGame = GameRepository.LoadGame(GameRepository.GetGameNames()[gameNo]);
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

            var chosenConfig = ConfigRepository.GetConfigurationByName(
                ConfigRepository.GetConfigurationNames()[configNo]
            ); 
        
            gameInstance = new TicTacTwoBrain(chosenConfig);
        }
        
        do
        {
            Visualizer.DrawBoard(gameInstance);
    
            Console.Write("Please enter coordinates to place piece <x, y>:");
            var input = Console.ReadLine()!;
            var inputSplit = input.Split(",");
            var inputX = int.Parse(inputSplit[0]);
            var inputY = int.Parse(inputSplit[1]);
            gameInstance.MakeAMove(inputX, inputY);
            
        } while (true);
        
    }

    private static string ChooseConfiguration()
    {
        var configMenuItems = new List<MenuItem>();

        for (int i = 0; i < ConfigRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(new MenuItem()
            {
                Title = ConfigRepository.GetConfigurationNames()[i],
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

    private static string ChooseSavedGame()
    {
        var gameMenuItems = new List<MenuItem>();

        for (var i = 0; i < GameRepository.GetGameNames().Count; i++)
        {
            var returnValue = i.ToString();
            gameMenuItems.Add(new MenuItem()
            {
                Title = GameRepository.GetGameNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
        var gameMenu = new Menu(
            "Choose saved game", 
            gameMenuItems, 
            EMenuLevel.Secondary, 
            isCustomMenu: true);
        
        return gameMenu.Run();
    }
    
}
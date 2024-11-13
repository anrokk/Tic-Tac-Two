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
    
            Console.WriteLine("Choose the coordinates <x,y>: ");
            Console.WriteLine("Press 'm' to move the grid");
            Console.WriteLine("Press 'e' to exit");
            var input = Console.ReadLine()!;

            if (GetUserInput(input, gameInstance))
            {
                break;
            }
            
        } while (true);

        return "Exit";
    }

    private static bool GetUserInput(string input, TicTacTwoBrain gameInstance)
    {
        if (input.Equals("E", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Exiting from the game...");
            return true;
        }
        
        if (input.Equals("M", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Choose one of the directions to move the grid: 'up', 'down', 'left', 'right', 'up-left', 'up-right', 'down-left' or 'down-right'");
            var inputDirection = Console.ReadLine()!;
            gameInstance.MoveGrid(inputDirection);
            return false;
        }

        if (input.Equals("save", StringComparison.CurrentCultureIgnoreCase))
        {
            GameRepository.SaveGame(gameInstance.GetGameStateJson(), gameInstance.GetGameConfigName());
            Console.WriteLine("The game has been saved!");
            return false;
        }
        
        var inputSplit = input.Split(",");
        if (int.TryParse(inputSplit[0], out var x) && int.TryParse(inputSplit[1], out var y))
        {
            gameInstance.MakeAMove(x, y);
        }
        else
        {
            Console.WriteLine("Input is invalid. Input must be a number! Please try again. <x,y>: ");
        }

        return false;
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
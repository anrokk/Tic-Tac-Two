using DAL;
using GameLogic;
using MenuSystem;
using ConsoleUI;

namespace ConsoleApp;

public class GameController(IConfigRepository configRepository, IGameRepository gameRepository, string username)
{
    
    public string StartNewGame()
    {
        return MainLoop();
    }

    public string LoadSavedGame()
    {
        var chosenGameId = ChooseSavedGame();
        var chosenGame = gameRepository.LoadGame(chosenGameId, username);
        var gameInstance = new TicTacTwoBrain(chosenGame);
        
        return MainLoop(gameInstance);
    }
    
    private static string MainLoop(TicTacTwoBrain? gameInstance = null)
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
            
            DisplayGameOptions();
            
            var input = Console.ReadLine()!;

            if (GetUserInput(input, gameInstance))
            {
                break;
            }
            
        } while (true);

        return "Exit";
    }

    private static void DisplayGameOptions()
    {
        Console.WriteLine("Press 'm' to move the grid");
        Console.WriteLine("Press 'e' to exit");
        Console.WriteLine("Write 'save' to save the game");
        Console.WriteLine("Choose the coordinates <x,y>: ");
        Console.WriteLine();
        Console.Write(">>>");
    }

    private static bool GetUserInput(string input, TicTacTwoBrain gameInstance)
    {
        
        if (input.Equals("M", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Choose one of the directions to move the grid: 'up', 'down', 'left', 'right', 'up-left', 'up-right', 'down-left' or 'down-right'");
            Console.Write(">>>");
            var inputDirection = Console.ReadLine()!;
            gameInstance.MoveGrid(inputDirection);
            return false;
        }
        
        if (input.Equals("E", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Exiting from the game...");
            return true;
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

        for (var i = 0; i < ConfigRepository.GetConfigurationNames().Count; i++)
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
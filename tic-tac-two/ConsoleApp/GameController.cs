using DAL;
using GameLogic;
using MenuSystem;

namespace ConsoleApp;

public static class GameController
{

    private static readonly ConfigRepository ConfigRepository = new ConfigRepository();
    
    public static string MainLoop()
    {
        var chosenConfigShortcut = ChooseConfiguration();
        if (!int.TryParse(chosenConfigShortcut, out var configNo))
        {
            return chosenConfigShortcut;
        }

        var chosenConfig = ConfigRepository.GetConfigurationByName(
            ConfigRepository.GetConfigurationNames()[configNo]
        );
    
        var gameInstance = new TicTacTwoBrain(chosenConfig);
        
        do
        {
            ConsoleUI.Visualize.DrawBoard(gameInstance);
    
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
    
        var configMenu = new Menu(EMenuLevel.Deep,
            "TIC-TAC-TWO - Choose Game Config",
            configMenuItems
        );

        return configMenu.Run();
    }
    
}
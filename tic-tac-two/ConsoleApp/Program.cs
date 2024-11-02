// See https://aka.ms/new-console-template for more information


using DAL;
using GameLogic;
using MenuSystem;

var configRepository = new ConfigRepository();


var deepMenu = new Menu(EMenuLevel.Deep,
    "TIC-TAC-TWO",
    [
        new MenuItem()
        {
            Shortcut = "X",
            Title = "X",
            MenuItemAction = null
        }
    ]);

var optionsMenu = new Menu(EMenuLevel.Secondary,
    "TIC-TAC-TWO | Options Menu",
    [
        new MenuItem()
        {
            Shortcut = "X",
            Title = "Player X starts",
            MenuItemAction =  deepMenu.Run
        },
        new MenuItem()
        {
            Shortcut = "O",
            Title = "Player O starts",
            MenuItemAction =  null
        },
    ]);


var mainMenu = new Menu(EMenuLevel.Main,
    "TIC-TAC-TWO | Main Menu", 
    [
    new MenuItem()
    {
        Shortcut = "N",
        Title = "New Game",
        MenuItemAction = NewGame
    },
    
    new MenuItem()
    {
        Shortcut = "L",
        Title = "Load Game",
        MenuItemAction = null
    },
    
    new MenuItem()
    {
        Shortcut = "O",
        Title = "Options",
        MenuItemAction = optionsMenu.Run
    }
]);


mainMenu.Run();


// ===================


string NewGame()
{
    var configMenuItems = new List<MenuItem>();

    for (int i = 0; i < configRepository.GetConfigurationNames().Count; i++)
    {
        configMenuItems.Add(new MenuItem()
        {
            Title = configRepository.GetConfigurationNames()[i],
            Shortcut = i.ToString()
        });
    }
    
    var configMenu = new Menu(EMenuLevel.Deep,
        "TIC-TAC-TWO - Choose Game Config",
        configMenuItems
    );

    var chosenConfigShortcut = configMenu.Run();

    if (!int.TryParse(chosenConfigShortcut, out var configNo))
    {
        return chosenConfigShortcut;
    }

    var chosenConfig = configRepository.GetConfigurationByName(
            configRepository.GetConfigurationNames()[configNo]
    );
    
    var gameInstance = new TicTacTwoBrain(chosenConfig);

    ConsoleUI.Visualize.DrawBoard(gameInstance);
    
    Console.Write("Please enter coordinates to place piece <x, y>:");

    var input = Console.ReadLine()!;
    var inputSplit = input.Split(",");
    var inputX = int.Parse(inputSplit[0]);
    var inputY = int.Parse(inputSplit[1]);
    gameInstance.MakeAMove(inputX, inputY);
    
    
    
    return "";
}






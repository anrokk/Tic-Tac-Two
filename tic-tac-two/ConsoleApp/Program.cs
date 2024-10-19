// See https://aka.ms/new-console-template for more information


using GameLogic;
using MenuSystem;

var gameConfigurations = new List<GameConfiguration>()
{
    new GameConfiguration()
    {
        Name = "Classical Tic-Tac-Two"
    },
    new GameConfiguration()
    {
        Name = "To Be Implemented"
    },

};

var gameInstance = new TicTacTwoBrain(5);


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

    ConsoleUI.Visualize.DrawBoard(gameInstance);
    
    Console.Write("Please enter coordinates to place piece <x, y>:");

    var input = Console.ReadLine()!;
    var inputSplit = input.Split(",");
    var inputX = int.Parse(inputSplit[0]);
    var inputY = int.Parse(inputSplit[1]);
    gameInstance.MakeAMove(inputX, inputY);
    
    
    
    return "";
}






// See https://aka.ms/new-console-template for more information


using GameLogic;
using MenuSystem;

var gameInstance = new TicTacTwoBrain();


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

string DrawGamePiece(EGamePiece piece)
{
    return piece switch
    {
        EGamePiece.O => "O",
        EGamePiece.X => "X",
        _ => " "
    };
}

string NewGame()
{
    for (int y = 0; y < gameInstance.DimensionY; y++)
    {
        for (int x = 0; x < gameInstance.DimensionX; x++)
        {
            Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
            if (x != gameInstance.DimensionX -1)
            {
                Console.Write("|");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine("   |   |   |   |   ");
    Console.WriteLine("---+---+---+---+---");
    Console.WriteLine("   |   |   |   |   ");
    Console.WriteLine("---+---+---+---+---");
    Console.WriteLine("   |   |   |   |   ");
    Console.WriteLine("---+---+---+---+---");
    Console.WriteLine("   |   |   |   |   ");
    Console.WriteLine("---+---+---+---+---");
    Console.WriteLine("   |   |   |   |   ");
    return "";
}






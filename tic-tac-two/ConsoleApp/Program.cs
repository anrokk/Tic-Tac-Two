// See https://aka.ms/new-console-template for more information


using MenuSystem;

var mainMenu = new Menu("TIC-TAC-TWO", [
    new MenuItem()
    {
        Shortcut = "O",
        Title = "Options"
    },

    new MenuItem()
    {
        Shortcut = "N",
        Title = "New Game"
    }
]);

mainMenu.Run();

return;
// ===================



static void MainMenu()
{
    MenuStart();
    Console.WriteLine("O) Options");
    Console.WriteLine("N) New Game");
    Console.WriteLine("L) Load game");
    Console.WriteLine("E) Exit");
    MenuEnd();
}

static void MenuOptions()
{
    MenuStart();
    Console.WriteLine("Choose an option for player one:");
    Console.WriteLine("Choose an option for player two:");
    MenuEnd();
}

static void MenuEnd()
{
    Console.WriteLine("");
    Console.WriteLine(">"); 
}

static void MenuStart()
{
    Console.Clear();
    Console.WriteLine("TIC-TAC-TWO");
    Console.WriteLine("===================");
}


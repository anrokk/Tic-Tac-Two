using ConsoleApp;
using DAL;
using DAL.Database;

// Game starts here

var databaseInitializer = new DatabaseInitializer();
databaseInitializer.Initialize();

var dbContext = databaseInitializer.GetDbContext();

IConfigRepository configRepository = new ConfigRepositoryDb(dbContext);
IGameRepository gameRepository = new GameRepositoryDb(dbContext);
 
// JSON APPROACH
 
// IConfigRepository configRepository = new ConfigRepositoryJson();
// IGameRepository gameRepository = new GameRepositoryJson();
 
var username = AskForUsername();
var menu = new Menus(configRepository, gameRepository, username);
Thread.Sleep(1000);
menu.MainMenu.Run();

return;

string AskForUsername()
{
    Console.WriteLine("Welcome to TIC-TAC-TWO!");
    Console.Write("Choose an username: ");
    var name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Username cannot be empty. Please try again.");
        Console.Write("Choose an username: ");
        name = Console.ReadLine();
    }
    Console.Clear();
    Console.WriteLine($"Hello, {name}! Let's play Tic-Tac-Two!");
    return name;
}






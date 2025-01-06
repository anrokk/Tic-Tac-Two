using DAL;
using MenuSystem;

namespace ConsoleApp;

public class Menus
{
    private Menu OptionsMenu { get; }
    public Menu MainMenu { get; }

    public Menus(IConfigRepository configRepository, IGameRepository gameRepository, string username)
    {
        var optionsController = new OptionsController(configRepository, username);
        var gameController = new GameController(configRepository, gameRepository, username);
        
        OptionsMenu =
            new Menu("Options", [
                new MenuItem()
                {
                    Title = "Create a new configuration",
                    Shortcut = "C",
                    MenuItemAction = optionsController.NewConfiguration
                },
            ], EMenuLevel.Secondary);
        
        var menuItems = new List<MenuItem>
        {
            new()
            {
                Title = "New Game",
                Shortcut = "N",
                MenuItemAction = gameController.StartNewGame
            },
            new()
            {
                Title = "Options",
                Shortcut = "O",
                MenuItemAction = OptionsMenu.Run
            }
        };
        
        var savedGames = gameRepository.GetAllGameNames(username);
        if (savedGames.Count != 0)
        {
            menuItems.Add(new MenuItem()
            {
                Title = "Load Game",
                Shortcut = "L",
                MenuItemAction = gameController.LoadSavedGame
            });
        }
        
        MainMenu = new Menu("TIC-TAC-TWO", menuItems, EMenuLevel.Main);
    }
}
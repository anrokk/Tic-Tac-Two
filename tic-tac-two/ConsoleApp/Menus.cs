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
        
        MainMenu =
            new Menu("TIC-TAC-TWO", [
                new MenuItem()
                {
                    Title = "New Game",
                    Shortcut = "N",
                    MenuItemAction = gameController.StartNewGame
                },
                new MenuItem()
                {
                    Title = "Load Game",
                    Shortcut = "L",
                    MenuItemAction = gameController.LoadSavedGame
                },
                new MenuItem()
                {
                    Title = "Options",
                    Shortcut = "O",
                    MenuItemAction = OptionsMenu.Run
                }
            ], EMenuLevel.Main);
    }
}
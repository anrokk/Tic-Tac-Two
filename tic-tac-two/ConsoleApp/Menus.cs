using MenuSystem;

namespace ConsoleApp;

public static class Menus
{
    public static readonly Menu OptionsMenu =
        new Menu(EMenuLevel.Secondary,
        "TIC-TAC-TWO | Options Menu",
        [
            new MenuItem()
            {
                Shortcut = "X",
                Title = "Player X starts",
                MenuItemAction = null
            },
            new MenuItem()
            {
                Shortcut = "O",
                Title = "Player O starts",
                MenuItemAction =  null
            },
        ]);
    
    public static Menu MainMenu =
        new Menu(EMenuLevel.Main,
        "TIC-TAC-TWO | Main Menu", 
        [
            new MenuItem()
            {
                Shortcut = "N",
                Title = "New Game",
                MenuItemAction = GameController.MainLoop
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
                MenuItemAction = OptionsMenu.Run
            }
        ]);
}
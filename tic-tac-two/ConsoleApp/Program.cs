// See https://aka.ms/new-console-template for more information


using MenuSystem;


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
        MenuItemAction = null
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






// See https://aka.ms/new-console-template for more information


using MenuSystem;

var mainMenu = new Menu("TIC-TAC-TWO", [
    
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
        MenuItemAction = null
    },
    
    new MenuItem()
    {
        Shortcut = "E",
        Title = "Exit"
    }
]);

mainMenu.Run();


// ===================






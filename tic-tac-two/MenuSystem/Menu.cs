namespace MenuSystem;

public class Menu
{
    private string MenuHeader { get; }
    private const string MenuDivider = "------------------------";
    private List<MenuItem> MenuItems { get; }
    private EMenuLevel EMenuLevel { get; }
    
    private bool IsCustomMenu { get; }
    
    public void SetMenuItemAction(string shortcut, Func<string> action)
    {
        var menuItem = MenuItems.Single(m => m.Shortcut == shortcut);
        menuItem.MenuItemAction = action;
    }
    
    private readonly MenuItem _menuItemExit = new ()
    {
        Shortcut = "E",
        Title = "Exit",
    };
    
    private readonly MenuItem _menuItemReturn = new ()
    {
        Shortcut = "R",
        Title = "Return",
    };
    
    private readonly MenuItem _menuItemReturnMain = new ()
    {
        Shortcut = "M",
        Title = "Return to Main Menu",
    };
    
    public Menu(string menuHeader, List<MenuItem> menuItems, EMenuLevel menuLevel, bool isCustomMenu = false)
    {
        if (string.IsNullOrWhiteSpace(menuHeader))
        {
            throw new ApplicationException("Menu header cannot be empty");
        }
        
        MenuHeader = menuHeader;
        
        if (menuItems == null || menuItems.Count == 0)
        {
            throw new ApplicationException("Menu items cannot be empty");
        }
        
        MenuItems = menuItems;
        EMenuLevel = menuLevel;
        IsCustomMenu = isCustomMenu;
        
        if (EMenuLevel != EMenuLevel.Main)
        {
            MenuItems.Add(_menuItemReturn);
        }
        
        if (EMenuLevel == EMenuLevel.Deep)
        {
            MenuItems.Add(_menuItemReturnMain);
        }
        
        MenuItems.Add(_menuItemExit);
    }

    public string Run()
    {
        Console.Clear();

        do
        {
            var menuItem = DisplayMenuGetUserChoice();
            var menuReturnValue = "";
        
            if (menuItem.MenuItemAction != null)
            {
                menuReturnValue = menuItem.MenuItemAction();
                if (IsCustomMenu)
                {
                    return menuReturnValue;
                }
            }

            if (menuItem.Shortcut == _menuItemReturn.Shortcut)
            {
                return menuItem.Shortcut;
            }
            
            if (menuItem.Shortcut == _menuItemExit.Shortcut || menuReturnValue == _menuItemExit.Shortcut)
            {
                return _menuItemExit.Shortcut;
            }

            if ((menuItem.Shortcut == _menuItemReturnMain.Shortcut || menuReturnValue == _menuItemReturnMain.Shortcut) 
                && EMenuLevel != EMenuLevel.Main)
            {
                return _menuItemReturnMain.Shortcut;
            }
            
            if (!string.IsNullOrWhiteSpace(menuReturnValue))
            {
                return menuReturnValue;
            }
            
        } while (true);
    }

    private MenuItem DisplayMenuGetUserChoice()
    {
        do
        {
            DrawMenu();
            
            var userInput = Console.ReadLine();
            
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please choose an option");
                Console.WriteLine();
            }
            else
            {
                userInput = userInput.ToUpper();
                
                foreach (var menuItem in MenuItems.Where(menuItem => menuItem.Shortcut.Equals(userInput)))
                {
                    return menuItem;
                } 
            }
            
            Console.WriteLine("Please choose a valid option");
            Console.WriteLine();

        } while (true);
    }
    
    private void DrawMenu()
    {
        Console.Clear();
        Console.WriteLine(MenuHeader);
        Console.WriteLine(MenuDivider);
        foreach (var menuItem in MenuItems)
        {
            Console.WriteLine(menuItem);
        }
        Console.WriteLine();
        Console.Write(">>>");
    }
}
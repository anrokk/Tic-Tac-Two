namespace MenuSystem;

public class Menu
{
    private string MenuHeader { get; set; }
    private static string _menuDivider = "-------------";
    private List<MenuItem> MenuItems { get; set; }
    

    public Menu(string menuHeader, List<MenuItem> menuItems)
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
        
    }

    public void Run()
    {
        Console.Clear();

        var menuItem = DisplayMenuGetUserChoice();

    }

    private MenuItem DisplayMenuGetUserChoice()
    {
        var userInput = "";
        
        do
        {
            DrawMenu();
            
            userInput = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Please choose an option");
                Console.WriteLine();
            }
            else
            {
                userInput = userInput.ToUpper();
                
                foreach (var menuItem in MenuItems)
                {
                    if (menuItem.Shortcut.ToUpper() != userInput) continue;
                    return menuItem;
                } 
                
                Console.WriteLine("Please choose a valid option"); 
                Console.WriteLine();
            }
        } while (true);
    }
    

    private void DrawMenu()
    {
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);

        foreach (var t in MenuItems)
        {
            Console.WriteLine(t);
        }
        
        Console.WriteLine();
        
        Console.Write(">>>");
    }


}
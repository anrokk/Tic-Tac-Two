namespace MenuSystem;

public class Menu
{
    private string MenuHeader { get; set; }
    private static string _menuDivider = "-------------";
    private List<MenuItem> MenuItems { get; set; }
    

    public Menu(string header, List<MenuItem> menuItems)
    {

        if (string.IsNullOrWhiteSpace(header))
        {
            throw new ApplicationException("Menu header cannot be empty");
        }
        
        MenuHeader = header;
        
        if (menuItems == null || menuItems.Count == 0)
        {
            throw new ApplicationException("Menu items cannot be empty");
        }
        
        MenuItems = menuItems;
        
    }

    public void Run()
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
                var userInputOk = false;
                
                foreach (var menuItem in MenuItems)
                {
                    if (menuItem.Shortcut.ToUpper() != userInput) continue;
                    userInputOk = true;
                    break;
                }

                if (userInputOk == false)
                {
                    userInput = "";
                    Console.WriteLine("Please choose an option");
                    Console.WriteLine();
                }
            }
            
        } while (string.IsNullOrWhiteSpace(userInput));
    }

    private void DrawMenu()
    {
        Console.Clear();
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);

        foreach (var menuItem in MenuItems)
        {
            Console.WriteLine(menuItem);
        }
        
        Console.WriteLine();
        
        Console.Write(">");
    }


}
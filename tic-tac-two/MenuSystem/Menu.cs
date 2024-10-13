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
        Console.Clear();
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);

        foreach (var t in MenuItems)
        {
            Console.WriteLine(t);
        }
        
        Console.WriteLine();
        
        Console.Write(">");

        var userInput = Console.ReadLine();

    }


}
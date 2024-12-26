using DAL;
using Domain;
using GameLogic;

namespace ConsoleApp;

public class OptionsController
{

    private readonly IConfigRepository _configRepository;

    public OptionsController (IConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }
    
    public string NewConfiguration()
    {
        var configurationName = AskConfigurationName();
        var boardInputSplit = AskBoardSize();
        var boardWidth = boardInputSplit[0];
        var boardHeight = boardInputSplit[1];
        var gridInputSplit = AskGridSize();
        var gridWidth = gridInputSplit[0];
        var gridHeight = gridInputSplit[1];
        var winCondition = AskWinCondition();
        var moveAfterNPieces = AskMoveAfterNPieces();

        var newConfiguration = new GameConfiguration()
        {
            Name = configurationName,
            BoardSizeWidth = boardWidth, 
            BoardSizeHeight = boardHeight, 
            GridSizeWidth = gridWidth,
            GridSizeHeight = gridHeight,
            WinCondition = winCondition,
            MovePieceAfterNMoves = moveAfterNPieces, 
        };

        _configRepository.SaveConfiguration(newConfiguration);
        
        return "";
    }
    
    private static string AskConfigurationName()
    {
        do
        {
            Console.WriteLine("Please enter the name of the configuration: ");
            Console.Write(">");
            
            var userInput = Console.ReadLine()!;
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Configuration name cannot be empty. Please write something");
            }
            else
            {
                return userInput;
            }
            
        } while (true);
    }
    
    private static int[] AskBoardSize()
    {
        do
        {
            Console.WriteLine("Please enter the size of the board: <width>x<height>");
            Console.WriteLine("Do not forget to write x also");
            Console.Write(">");
            
            var userInput = Console.ReadLine()!;

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please enter the size of the board: <width>x<height>");
            }
            else
            {
                var parts = userInput.Split("x");
                if (int.TryParse(parts[0], out var boardWidth) && int.TryParse(parts[1], out var boardHeight))
                {
                    return [boardWidth, boardHeight];
                }
                
                Console.WriteLine("Please enter int type of values");
            }
        } while (true);
    }

    private static int[] AskGridSize()
    {
        do
        {
            Console.WriteLine("Please enter the size of the grid: <width>x<height>");
            Console.WriteLine("Do not forget to write x also");
            Console.Write(">");
            
            var userInput = Console.ReadLine()!;

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please enter the size of the grid: <width>x<height>");
            }
            else
            {
                var parts = userInput.Split("x");
                if (int.TryParse(parts[0], out var gridWidth) && int.TryParse(parts[1], out var gridHeight))
                {
                    return [gridWidth, gridHeight];
                }
                Console.WriteLine("Please enter int type of values");
            }

        } while (true);
    }
    
    private static int AskWinCondition()
    {
        do
        {
            Console.WriteLine("Win condition: ");
            Console.WriteLine("How many pieces in a row horizontally/vertically/diagonally to win the game?");
            Console.Write(">");
            
            var userInput = Console.ReadLine()!;
            
            if (string.IsNullOrEmpty(userInput))
            {
                return 3; // by default
            }

            if (int.TryParse(userInput, out var winCondition))
            {
                return winCondition;
            }
            
            Console.WriteLine("Invalid input. Try again.");
            
        } while (true);
    }

    private static int AskMoveAfterNPieces()
    {
        do
        {
            Console.WriteLine("After how many moves should the players be able to move pieces?");
            Console.Write(">");
            
            var userInput = Console.ReadLine()!;

            if (string.IsNullOrEmpty(userInput))
            {
                return 3; // by default
            }

            if (int.TryParse(userInput, out var moveAfterNPieces))
            {
                return moveAfterNPieces;
            }
            
            Console.WriteLine("Invalid input. Try again.");

        } while (true);
    }
    
}
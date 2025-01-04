using Domain;

namespace GameLogic;

public static class MoveValidator
{
    public static List<string> ValidateMakeAMove
    (int x, int y, GameState gameState, GameConfiguration gameConfiguration, bool movePiece)
    {
        List<string> errors = [];

        if (!movePiece && gameState.GetMovesMade() == gameConfiguration.MovePieceAfterNMoves * 2)
        {
            Console.WriteLine("You are out of pieces. Move the grid or move a piece.");
            errors.Add("You are out of pieces. Move the grid, or move a piece.");
        }

        if (x < gameState.GridStartX || x > gameState.GridEndX || y < gameState.GridStartY || y > gameState.GridEndY)
        {
            Console.WriteLine("You can only move within the grid.");
            errors.Add("You can only move within the grid.");
        }
        
        else if (gameState.GameBoard[x][y] != EGamePiece.Empty)
        {
            Console.WriteLine("There is already a piece there.");
            errors.Add("There is already a piece there.");
        }

        return errors;
    }

    public static string ValidateMovePiece(int x, int y, GameState gameState)
    {
        if (gameState.GameBoard[x][y] == gameState.NextMoveBy) return null!;
        Console.WriteLine($"Your piece is not in that place ({x}, {y}).");
        return "Your piece is not in that place.";
    }

    public static bool CheckMoveInput(TicTacTwoBrain gameInstance, string[] inputSplit)
    {
        if (inputSplit.Length != 2)
        {
            Console.WriteLine("The input should be in the format <x,y>.");
        }

        if (!int.TryParse(inputSplit[0], out var inputX))
        {
            Console.WriteLine($"{inputX} is not a valid number. Format should be <x,y>.");
        }

        if (!int.TryParse(inputSplit[1], out var inputY))
        {
            Console.WriteLine($"{inputY} is not a valid number. Format should be <x,y>.");
        }

        if (inputX < 0 || inputX > gameInstance.DimensionX)
        {
            Console.WriteLine($"Value {inputX} is out of range for X. Format should be <x,y>.");
        }

        if (inputY < 0 || inputY > gameInstance.DimensionY)
        {
            Console.WriteLine($"Value {inputY} is out of range for Y. Format should be <x,y>.");
        }

        gameInstance.MakeAMove(inputX, inputY);

        return false;
    }
    
    public static bool CheckMovePieceInput(TicTacTwoBrain gameInstance, string[] inputCoordinates)
    {
        if (inputCoordinates.Length != 4)
        {
            Console.WriteLine("The input format is x,y x,y. Try again '<x,y> <x,y>': ");
            return false;
        }
        
        if (!int.TryParse(inputCoordinates[0], out var currentX))
        {
            Console.WriteLine($"'{currentX}' seems not to be a number. Try again <x,y>: ");
            return false;
        }
        
        if (!int.TryParse(inputCoordinates[1], out var currentY))
        {
            Console.WriteLine($"'{currentY}' seems not to be a number. Try again <x,y>: ");
            return false;
        }
        
        if (!int.TryParse(inputCoordinates[2], out var newX))
        {
            Console.WriteLine($"'{newX}' seems not to be a number. Try again <x,y>: ");
            return false;
        }
        
        if (!int.TryParse(inputCoordinates[3], out var newY))
        {
            Console.WriteLine($"'{newY}' seems not to be a number. Try again <x,y>: ");
            return false;
        }
        
        if (newX < 0 || newX > gameInstance.DimensionX)
        {
            Console.WriteLine($"Value '{newX}' is out of range for X. Try again <x,y>: ");
            return false;
        }
        
        if (newY < 0 || newY > gameInstance.DimensionY)
        {
            Console.WriteLine($"Value '{newY}' is out of range for Y. Try again <x,y>: ");
            return false;
        }
        
        gameInstance.MovePiece(currentX, currentY, newX, newY);
        return false;
    } 
}
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
}
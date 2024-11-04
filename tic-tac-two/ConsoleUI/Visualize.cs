using GameLogic;

namespace ConsoleUI;

public static class Visualize
{
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        for (var y = 0; y < gameInstance.DimensionY; y++)
        {
            for (var x = 0; x < gameInstance.DimensionX; x++)
            {
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
                
                if (x != gameInstance.DimensionX - 1)
                {
                    Console.Write("|");
                }
            }
            
            Console.WriteLine();
            
            if (y != gameInstance.DimensionY - 1)
            {
                for (var x = 0; x < gameInstance.DimensionX; x++)
                {
                    Console.Write("---");
                    if (x != gameInstance.DimensionX - 1)
                    {
                        Console.Write("+");
                    }
                } 
                Console.WriteLine();
            }
        }
    }
    
    private static string DrawGamePiece(EGamePiece piece)
    {
        return piece switch
        {
            EGamePiece.O => "O",
            EGamePiece.X => "X",
            _ => " "
        };
    }
}
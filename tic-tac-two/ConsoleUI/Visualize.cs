using GameLogic;

namespace ConsoleUI;

public static class Visualize
{
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        int gridOffset = 1;
        int gridWidth = gameInstance.DimensionX - 2;
        int gridHeight = gameInstance.DimensionY - 2;
        
        for (var y = 0; y < gameInstance.DimensionY; y++)
        {
            for (var x = 0; x < gameInstance.DimensionX; x++)
            {
                bool isInsideGrid = x >= gridOffset && x < gridOffset + gridWidth - 1 
                                                    && y >= gridOffset && y < gridOffset + gridHeight;
                if (isInsideGrid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
                
                if (x != gameInstance.DimensionX - 1)
                {
                    Console.Write("|");
                }
                
                Console.ResetColor();
            }

            Console.WriteLine();
            
            if (y != gameInstance.DimensionY - 1)
            {
                for (var x = 0; x < gameInstance.DimensionX; x++)
                {
                    
                    bool isInsideGrid = x >= gridOffset && x < gridOffset + gridWidth 
                                                        && y >= gridOffset && y < gridOffset + gridHeight - 1;

                    if (isInsideGrid)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                    Console.Write("---");
                    if (x != gameInstance.DimensionX - 1)
                    {
                        Console.Write("+");
                    }
                    
                    Console.ResetColor();
                    
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
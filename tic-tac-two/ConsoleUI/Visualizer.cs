using Domain;
using GameLogic;

namespace ConsoleUI;

public static class Visualizer
{
    public static string DrawGamePiece(EGamePiece piece)
    {
        return piece switch
        {
            EGamePiece.O => "O",
            EGamePiece.X => "X",
            _ => " "
        };
    }

    private const string Reset = "\u001b[0m";
    private const string Highlight = "\u001b[42m";
    
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        var gridStartX = gameInstance.GridStartX;
        var gridStartY = gameInstance.GridStartY;
        var gridEndX = gameInstance.GridEndX;
        var gridEndY = gameInstance.GridEndY;
        
        Console.Write("   ");
        for (var x = 0; x < gameInstance.DimensionX; x++)
        {
            Console.Write($" {x}  ");
        }
        Console.WriteLine();
        
        for (var y = 0; y < gameInstance.DimensionY; y++)
        {
            Console.Write($"{y}  ");
            
            for (var x = 0; x < gameInstance.DimensionX; x++)
            {
                if (x >= gridStartX && x <= gridEndX && y >= gridStartY && y <= gridEndY)
                {
                    Console.Write(Highlight + " " + 
                                  DrawGamePiece(gameInstance.GameBoard[x][y]) + " " + Reset);
                }
                
                else
                {
                    Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x][y]) + " ");
                }
                
                if (x != gameInstance.DimensionX - 1)
                {
                    Console.Write("|");
                }
            }
            
            Console.WriteLine();
            
            if (y != gameInstance.DimensionY - 1)
            {
                Console.Write("   ");
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
}
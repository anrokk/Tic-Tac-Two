using Domain;
using GameLogic;

namespace ConsoleUI;

public static class Visualizer
{
    private const string GridBackgroundColor = "\u001b[47m";
    
    private const string PieceOColor = "\u001b[34m";
    
    private const string PieceXColor = "\u001b[31m";
    
    private const string ResetToNormal = "\u001b[0m";
    
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

                var moveInGrid = x >= gridStartX && x <= gridEndX && y >= gridStartY && y <= gridEndY;

                if (moveInGrid)
                {
                    Console.Write(GridBackgroundColor +
                                  " " +
                                  DrawGamePiece(gameInstance.GameBoard[x][y]) +
                                  " " +
                                  ResetToNormal
                    );
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
    private static string DrawGamePiece(EGamePiece piece)
    {
        return piece switch
        {
            EGamePiece.O => PieceOColor + "O" + ResetToNormal,
            EGamePiece.X => PieceXColor + "X" + ResetToNormal,
            _ => " "
        };
    }
}
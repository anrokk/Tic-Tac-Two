using ConsoleUI;
using DAL;
using Domain;
using GameLogic;

namespace ConsoleApp;

public class GameLoops
{
    public static void PlayerAgainstPlayer(TicTacTwoBrain gameInstance, IGameRepository gameRepository, string username)
    {
        do
        {
            Visualizer.DrawBoard(gameInstance);

            Console.WriteLine(gameInstance.IsGameOver()
                ? "Game Over! Type 'save' to save the game or 'return' to exit to main menu without saving: "
                : $"{gameInstance.GetCurrentGamePiece()} Choose the coordinates or type 'mg' to move grid, 'mp' to move piece, Return(r)/Save(s) <x,y>: ");

            var userInput = Console.ReadLine()!;

            if (userInput.StartsWith("r", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Exiting to main menu...");
                Thread.Sleep(500);
                break;
            }

            if (userInput.StartsWith("s", StringComparison.CurrentCultureIgnoreCase))
            {
                //TODO

            }
        } while (true);
    }
}
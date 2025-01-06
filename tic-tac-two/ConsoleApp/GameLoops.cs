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
                gameRepository.SaveGame(gameInstance.GetGameState(), username);
                Console.WriteLine("Game has been saved!");
                continue;
            }

            if (GetUserMove(userInput, gameInstance)) break;
        }
        while (true) ;
    }
    
    public static void PlayerAgainstAi(TicTacTwoBrain gameInstance, IGameRepository gameRepository, string username)
    {
        var ai = new TicTacTwoAi(gameInstance, EGamePiece.O, EGamePiece.X);
        do
        {
            Visualizer.DrawBoard(gameInstance);
            Console.Write(gameInstance.IsGameOver() ?
                "Game Over! type 'save' to save the game, 'return' to exit to main menu without saving: " :
                $"{gameInstance.GetCurrentGamePiece()} Choose the coordinates or type 'mg' to move grid, 'mp' to move piece, Return(r)/Save(s) <x,y>: ");

            if (gameInstance.GetGameState().NextMoveBy == EGamePiece.O && !gameInstance.IsGameOver())
            {
                Console.WriteLine("AI is thinking...");
                Thread.Sleep(1000);
                ai.MakeAMove();
            }
            else
            {
                var input = Console.ReadLine()!;
            
                if (input.StartsWith("r", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
            
                if (input.StartsWith("s", StringComparison.CurrentCultureIgnoreCase))
                {
                    gameRepository.SaveGame(gameInstance.GetGameState(), username);
                    Console.WriteLine("Game saved!");
                    continue;
                }
            
                if (GetUserMove(input, gameInstance)) break;
            }
        } while (true);
    }
    
    public static void AiAgainstAi(TicTacTwoBrain gameInstance, IGameRepository gameRepository, string username)
    {
        var ai1 = new TicTacTwoAi(gameInstance, EGamePiece.X, EGamePiece.O);
        var ai2 = new TicTacTwoAi(gameInstance, EGamePiece.O, EGamePiece.X);

        do
        {
            Visualizer.DrawBoard(gameInstance);

            if (gameInstance.IsGameOver())
            {
                Console.Write("Game Over! type 'save' to save the game, 'return' to exit to main menu without saving: ");
                var input = Console.ReadLine()!;
            
                if (input.StartsWith("r", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
            
                if (input.StartsWith("s", StringComparison.CurrentCultureIgnoreCase))
                {
                    gameRepository.SaveGame(gameInstance.GetGameState(), username);
                    Console.WriteLine("Game saved!");
                    continue;
                }
                
            }
            if (gameInstance.GetGameState().NextMoveBy == EGamePiece.X)
            {
                Console.WriteLine("AI 1 is thinking...");
                Thread.Sleep(1000);
                ai1.MakeAMove();
            }
            else
            {
                Console.WriteLine("AI 2 is thinking...");
                Thread.Sleep(1000);
                ai2.MakeAMove();
            }
            
        } while (true);
        
    }

    private static bool GetUserMove(string input, TicTacTwoBrain gameInstance)
    {
        if (input.Equals("mg", StringComparison.CurrentCultureIgnoreCase))
        {
            if (gameInstance.GetGameState().GetMovesMade() / 2 < 
                gameInstance.GetGameState().GetGameConfiguration().MovePieceAfterNMoves)
            {
                Console.WriteLine($"You cannot move the grid yet. {gameInstance.GetGameState().GetGameConfiguration().MovePieceAfterNMoves} moves must be made");
                return false;
            }
            
            Console.Write("Choose a direction to move the grid | 'up', 'down', 'left', 'right', 'up-left', 'up-right', 'down-left' or 'down-right': ");
            var direction = Console.ReadLine()!;

            gameInstance.MoveGrid(direction);

            return false;
        }

        if (input.Equals("mp", StringComparison.CurrentCultureIgnoreCase))
        {
            if (gameInstance.GetGameState().GetMovesMade() / 2 <
                gameInstance.GetGameState().GetGameConfiguration().MovePieceAfterNMoves)
            {
                Console.WriteLine($"Cannot move a piece yet! {gameInstance.GetGameState().GetGameConfiguration().MovePieceAfterNMoves} moves must be made");
            }
            Console.Write("Choose the piece coordinates and new coordinates: <x,y> <x,y>");
            var userInput = Console.ReadLine()!;
            var inputCoordinates = userInput.Replace(" ", ",").Split(",");

            return MoveValidator.CheckMovePieceInput(gameInstance, inputCoordinates);
        }
        
        var inputSplit = input.Split(",");

        return MoveValidator.CheckMoveInput(gameInstance, inputSplit);
    }
}
using DAL;
using Domain;
using GameLogic;

namespace WebApp;

public class GameService(IGameRepository gameRepo, GameContext context)
{
    private TicTacTwoAi? _ai1;
    private TicTacTwoAi? _ai2;
    
    public (bool Success, string Message) PlayPvsAi(
        TicTacTwoBrain gameInstance,
        int? x, 
        int? y, 
        string? move,
        string username)
    {
        _ai1 ??= new TicTacTwoAi(gameInstance, EGamePiece.O, EGamePiece.X);

        if (gameInstance.IsGameOver())
        {
            return (true, "Game over!");
        }
    
        if (gameInstance.GetGameState().NextMoveBy == _ai1.Piece)
        {
            _ai1.MakeAMove();
            gameRepo.SaveGame(gameInstance.GetGameState(), username);
            
            return gameInstance.IsGameOver() ? (true, "AI wins!") : (true, "AI made a move");
        }

        var result = MakeUserMove(gameInstance, x, y, move, username);
        
        return result;
    }

    public (bool Success, string Message) PlayPvsP(
        TicTacTwoBrain gameInstance,
        int? x,
        int? y,
        string? move,
        string username)
    {
        return gameInstance.IsGameOver() ? (true, "Game over!") : MakeUserMove(gameInstance, x, y, move, username);
    }

    public (bool Success, string Message) PlayAivsAi(TicTacTwoBrain gameInstance, string username)
    {
        _ai1 ??= new TicTacTwoAi(gameInstance, EGamePiece.O, EGamePiece.X);
        _ai2 ??= new TicTacTwoAi(gameInstance, EGamePiece.X, EGamePiece.O);

        if (gameInstance.IsGameOver())
        {
            return (true, "Game over!");
        }
        
        if (gameInstance.GetGameState().NextMoveBy == _ai1.Piece)
        {
            _ai1.MakeAMove();
            gameRepo.SaveGame(gameInstance.GetGameState(), username);
            return gameInstance.IsGameOver() ? (true, "Game over! AI 1 wins!") :
                (true, "AI 2 made a move");
        }
        
        _ai2.MakeAMove();
        gameRepo.SaveGame(gameInstance.GetGameState(), username);
        return gameInstance.IsGameOver() ? (true, "Game over! AI 2 wins!") :
            (true, "AI 1 made a move");
    }
    
    private (bool Success, string Message) ProcessMove(TicTacTwoBrain gameInstance, int x, int y, string username)
    {
        var errors = MoveValidator.ValidateMakeAMove(x, y, gameInstance.GetGameState(), 
            gameInstance.GetGameState().GetGameConfiguration(), 
            false);
        
        if (errors.Count != 0)
        {
            var errorMessage = string.Join("\n", errors);
            return (false, errorMessage);
        }
        
        gameInstance.MakeAMove(x, y);
        gameRepo.SaveGame(gameInstance.GetGameState(), username);
        
        return gameInstance.IsGameOver() ?
            (true, $"Game over! {gameInstance.GetCurrentGamePiece()} Won!") :
            (true, "Move made successfully.");
    }

    private (bool Success, string Message) ProcessMoveGrid(TicTacTwoBrain gameInstance, string direction, string username)
    {
        if (!gameInstance.MoveGrid(direction))
        {
            return (false, "Invalid direction!");
        }
        
        gameRepo.SaveGame(gameInstance.GetGameState(), username);
        context.MovingGrid = false;

        return (true, "Grid Moved!");
    }

    private (bool Success, string Message) ProcessMovePiece(TicTacTwoBrain gameInstance, int x, int y, string username)
    {
        if (!context.SelectedX.HasValue || !context.SelectedY.HasValue)
        {
            if (ConsoleUI.Visualizer.DrawGamePiece(gameInstance.GameBoard[x][y]) == " ")
                return (false, "No piece at the selected location.");
            
            if (gameInstance.GameBoard[x][y] != gameInstance.GetCurrentGamePiece())
            {
                return (false, "Not your piece!");
            }
            
            context.SelectedX = x;
            context.SelectedY = y;
                
            return (true,
                $"Selected piece at ({context.SelectedX}, {context.SelectedY}). Now select the destination.");
        }

        var errors = MoveValidator.ValidateMakeAMove(x, y,
            gameInstance.GetGameState(), gameInstance.GetGameState().GetGameConfiguration(), true);
        
        if (errors.Count != 0)
        {
            var errorMessage = string.Join("\n", errors);
            return (false, errorMessage);
        }
        
        gameInstance.MovePiece(context.SelectedX.Value, context.SelectedY.Value, x, y);
        
        context.SelectedX = null;
        context.SelectedY = null;
        context.MovingPiece = false;
        gameRepo.SaveGame(gameInstance.GetGameState(), username);
        
        return gameInstance.IsGameOver() ?
            (true, $"Game over! {gameInstance.GetCurrentGamePiece()} Won!") :
            (true, "Piece moved!");
    }

    private (bool Success, string Message) MakeUserMove(TicTacTwoBrain gameInstance,
        int? x,
        int? y,
        string? move,
        string username)
    {
        (bool Success, string Message) result = (true, "");
        
        switch (move)
        {
            case "piece":
                if (gameInstance.GetGameState().GetMovesMade() / 2 <
                    gameInstance.GetGameState().GetGameConfiguration().MovePieceAfterNMoves)
                {
                    result = (false, "Cannot move a piece yet!");
                    break;
                }
                
                context.MovingPiece = true;
                
                result = x.HasValue && y.HasValue ?
                    ProcessMovePiece(gameInstance, x.Value, y.Value, username) :
                    (true, "You are moving a piece");
                break;

            case "grid":
                if (gameInstance.GetGameState().GetMovesMade() / 2 <
                    gameInstance.GetGameState().GetGameConfiguration().MovePieceAfterNMoves)
                {
                    result = (false, "Cannot move the grid yet!");
                    break;
                }
                
                context.MovingGrid = true;
                
                result = !string.IsNullOrEmpty(context.Direction) ?
                    ProcessMoveGrid(gameInstance, context.Direction, username) :
                    (true, "You are moving the grid");
                break;

            default:
                if (x.HasValue && y.HasValue)
                {
                    result = ProcessMove(gameInstance, x.Value, y.Value, username);
                }
                break;
        }
        return result;
    }
}
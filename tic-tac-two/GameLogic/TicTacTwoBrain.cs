using Domain;

namespace GameLogic;

public class TicTacTwoBrain
{
    private readonly GameState _gameState;
    
    public TicTacTwoBrain(GameConfiguration gameConfiguration)
    {
        var gameBoard = new EGamePiece[gameConfiguration.BoardSizeWidth][];
        for (var x = 0; x < gameBoard.Length; x++)
        {
            gameBoard[x] = new EGamePiece[gameConfiguration.BoardSizeHeight];
        }
        _gameState = new GameState(gameBoard, gameConfiguration);
        
        CenterGrid();
    }

    public EGamePiece GetCurrentGamePiece() => _gameState.NextMoveBy;

    public TicTacTwoBrain(GameState gameState)
    {
        _gameState = gameState;
    }

    public GameState GetGameState() => _gameState;
    
    public EGamePiece[][] GameBoard => GetBoard();

    public int GridStartX => _gameState.GridStartX;

    public int GridStartY => _gameState.GridStartY;

    public int GridEndX => _gameState.GridEndX;

    public int GridEndY => _gameState.GridEndY;
    
    public int DimensionX => _gameState.GameBoard.Length;
    public int DimensionY => _gameState.GameBoard.Length > 0 ? _gameState.GameBoard[0].Length : 0;
    
    private void CenterGrid()
    {
        var gridWidth = _gameState.GameConfiguration.GridSizeWidth;
        var gridHeight = _gameState.GameConfiguration.GridSizeHeight;

        _gameState.GridStartX = (DimensionX - gridWidth) / 2;
        _gameState.GridStartY = (DimensionY - gridHeight) / 2;
        _gameState.GridEndX = _gameState.GridStartX + gridWidth - 1;
        _gameState.GridEndY = _gameState.GridStartY + gridHeight - 1;
    }
    
    private EGamePiece[][] GetBoard()
    {
        var copyOfBoard = new EGamePiece[_gameState.GameBoard.GetLength(0)][];
        for (var x = 0; x < _gameState.GameBoard.Length; x++)
        {
            copyOfBoard[x] = new EGamePiece[_gameState.GameBoard[x].Length];
            for (var y = 0; y < _gameState.GameBoard[x].Length; y++)
            {
                copyOfBoard[x][y] = _gameState.GameBoard[x][y];
            }
        }

        return copyOfBoard;
    }

    public bool IsGameOver()
    {
        return _gameState.IsGameOver;
    }

    private void EndGame(EGamePiece winner)
    {
        _gameState.IsGameOver = true;
        Console.WriteLine($"{winner} has won the game! Congratulations!");
    }

    public bool MakeAMove(int x, int y, bool movePiece = false)
    {
        var errors =
            MoveValidator.ValidateMakeAMove
                (x, y, GetGameState(), _gameState.GetGameConfiguration(), movePiece);

        if (errors.Count != 0)
        {
            return false;
        }

        _gameState.GameBoard[x][y] = _gameState.NextMoveBy;
        
        if (!movePiece)
        {
            _gameState.IncreaseMoves();
            if (CheckForWinner(_gameState.NextMoveBy))
            {
                EndGame(_gameState.NextMoveBy);
                return true;
            }
            ToggleNextMove();
        }
        return true;
    }

    public void MovePiece(int currentX, int currentY, int newX, int newY)
    {
        MoveValidator.ValidateMovePiece(currentX, currentY, GetGameState());

        if (MakeAMove(newX, newY, true))
        {
            _gameState.GameBoard[currentX][currentY] = EGamePiece.Empty;
            if (CheckForWinner(_gameState.NextMoveBy))
            {
                EndGame(_gameState.NextMoveBy);
                return;
            }
            ToggleNextMove();
        }
    }

    public bool MoveGrid(string direction)
    {
        switch (direction.ToLower())
        {
            case "up":
                if (_gameState.GridStartY > 0)
                {
                    _gameState.GridStartY--;
                    _gameState.GridEndY--;
                    ToggleNextMove();
                    return true;
                }
                break;
            
            case "down":
                if (_gameState.GridEndY < DimensionY - 1)
                {
                    _gameState.GridStartY++;
                    _gameState.GridEndY++;
                    ToggleNextMove();
                    return true;
                }
                break;
            
            case "left":
                if (_gameState.GridStartX > 0)
                {
                    _gameState.GridStartX--;
                    _gameState.GridEndX--;
                    ToggleNextMove();
                    return true;
                }
                break;
            
            case "right":
                if (_gameState.GridEndX < DimensionX - 1)
                {
                    _gameState.GridStartX++;
                    _gameState.GridEndX++;
                    ToggleNextMove();
                    return true;
                }
                break;
            
            case "up-left":
                if (_gameState.GridStartY > 0 && _gameState.GridStartX > 0)
                {
                    _gameState.GridStartY--;
                    _gameState.GridEndY--;
                    _gameState.GridStartX--;
                    _gameState.GridEndX--;
                    ToggleNextMove();
                    return true;
                }
                break;
            
            case "up-right":
                if (_gameState.GridStartY > 0 && _gameState.GridEndX < DimensionX - 1)
                {
                    _gameState.GridStartY--;
                    _gameState.GridEndY--;
                    _gameState.GridStartX++;
                    _gameState.GridEndX++;
                    ToggleNextMove();
                    return true;
                }
                break;
            case "down-left":
                if (_gameState.GridEndY < DimensionY - 1 && _gameState.GridStartX > 0)
                {
                    _gameState.GridStartY++;
                    _gameState.GridEndY++;
                    _gameState.GridStartX--;
                    _gameState.GridEndX--;
                    ToggleNextMove();
                    return true;
                }
                break;

            case "down-right":
                if (_gameState.GridEndY < DimensionY - 1 && _gameState.GridEndX < DimensionX - 1)
                {
                    _gameState.GridStartY++;
                    _gameState.GridEndY++;
                    _gameState.GridStartX++;
                    _gameState.GridEndX++;
                    ToggleNextMove();
                    return true;
                }
                break;
            
            default:
                Console.WriteLine("Direction is invalid.");
                break;
        }
        return false;
    }

    private bool CheckForWinner(EGamePiece gamePiece)
    {
        var winCondition = _gameState.GameConfiguration.WinCondition;

        // Horizontal check
        for (var y = GridStartY; y < GridEndY; y++)
        {
            for (var x = GridStartX; x <= GridEndX - winCondition + 1; x++)
            {
                if (IsWinningLine(gamePiece, x, y, 1, 0, winCondition))
                {
                    return true;
                }
            }
        }

        // Vertical check
        for (var x = GridStartX; x <= GridEndX; x++)
        {
            for (var y = GridStartY; y <= GridEndY - winCondition + 1; y++)
            {
                if (IsWinningLine(gamePiece, x, y, 0, 1, winCondition))
                {
                    return true;
                }
            }
        }
        
        // Diagonal check (from top-left to bottom-right)
        for (var y = GridStartY; y <= GridEndY - winCondition + 1; y++)
        {
            for (var x = GridStartX; x <= GridEndX - winCondition + 1; x++)
            {
                if (IsWinningLine(gamePiece, x, y, 1, 1, winCondition))
                {
                    return true;
                }
            }
        }
        
        // Diagonal check (bottom-left to top-right
        for (var y = GridStartY + winCondition - 1; y <= GridEndY; y++)
        {
            for (var x = GridStartX; x <= GridEndX - winCondition + 1; x++)
            {
                if (IsWinningLine(gamePiece, x, y, 1, -1, winCondition))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsWinningLine(EGamePiece gamePiece, int startX, int startY, int stepX, int stepY, int winCondition)
    {
        for (var i = 0; i < winCondition; i++)
        {
            var x = startX + i * stepX;
            var y = startY + i * stepY;
            if (_gameState.GameBoard[x][y] != gamePiece)
            {
                return false;
            }
        }
        return true;
    }
    
    private void ToggleNextMove()
    {
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }
}
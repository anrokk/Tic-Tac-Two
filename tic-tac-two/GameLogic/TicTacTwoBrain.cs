namespace GameLogic;

public class TicTacTwoBrain
{
    private readonly GameState _gameState;
    
    public TicTacTwoBrain(GameConfiguration gameConfiguration)
    {
        var gameBoard = new EGamePiece[gameConfiguration.BoardSizeWidth][];
        for (int x = 0; x < gameBoard.Length; x++)
        {
            gameBoard[x] = new EGamePiece[gameConfiguration.BoardSizeHeight];
        }
        _gameState = new GameState(gameBoard, gameConfiguration);
        
        CenterGrid();
    }

    public TicTacTwoBrain(GameState gameState)
    {
        _gameState = gameState;
    }

    public string GetGameStateJson()
    {
        return _gameState.ToString();
    }

    public string GetGameConfigName()
    {
        return _gameState.GameConfiguration.Name;
    }

    public EGamePiece[][] GameBoard
    {
        get => GetBoard();
        private set => _gameState.GameBoard = value;
    }

    public int GridStartX
    {
        get => _gameState.GridStartX;
        private set => _gameState.GridStartX = value;
    }

    public int GridStartY
    {
        get => _gameState.GridStartY;
        private set => _gameState.GridStartY = value;
    }

    public int GridEndX
    {
        get => _gameState.GridEndX;
        private set => _gameState.GridEndX = value;
    }

    public int GridEndY
    {
        get => _gameState.GridEndY;
        private set => _gameState.GridEndY = value;
    }
    
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

    public bool MakeAMove(int x, int y)
    {
        if (x < _gameState.GridStartX || x > _gameState.GridEndX || 
            y < _gameState.GridStartY || y > _gameState.GridEndY)
        {
            Console.WriteLine("Move is outside of the grid.");
        }

        if (_gameState.GameBoard[x][y] != EGamePiece.Empty)
        {
            Console.WriteLine("This square is already occupied.");
            return false;
        }
        
        _gameState.GameBoard[x][y] = _gameState.NextMoveBy;
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        
        return true;
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
                    return true;
                }
                break;
            
            case "down":
                if (_gameState.GridEndY < DimensionY - 1)
                {
                    _gameState.GridStartY++;
                    _gameState.GridEndY++;
                    return true;
                }
                break;
            
            case "left":
                if (_gameState.GridStartX > 0)
                {
                    _gameState.GridStartX--;
                    _gameState.GridEndX--;
                    return true;
                }
                break;
            
            case "right":
                if (_gameState.GridEndX < DimensionX - 1)
                {
                    _gameState.GridStartX++;
                    _gameState.GridEndX++;
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
                    return true;
                }
                break;
            
            default:
                Console.WriteLine("");
                break;
        }
        return false;
    }

    public void ResetGame()
    {
        var gameBoard = new EGamePiece[_gameState.GameConfiguration.BoardSizeWidth][];
        
        for (var x = 0; x < gameBoard.Length; x++)
        {
            gameBoard[x] = new EGamePiece[_gameState.GameConfiguration.BoardSizeHeight];
        }
        
        _gameState.GameBoard = gameBoard;
        _gameState.NextMoveBy = EGamePiece.X;
        CenterGrid();
    }
}
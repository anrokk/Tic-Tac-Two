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
    
    public int DimensionX => _gameState.GameBoard.Length;
    public int DimensionY => _gameState.GameBoard.Length > 0 ? _gameState.GameBoard[0].Length : 0;


    private void CenterGrid()
    {
        //TODO
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
        //TO IMPROVE
        if (_gameState.GameBoard[x][y] != EGamePiece.Empty)
        {
            return false;
        }
        _gameState.GameBoard[x][y] = _gameState.NextMoveBy;
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return true;
    }

    public bool MoveGrid(string direction)
    {
        //TODO
        return true;
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
        
        //TO REFERENCE CENTER GRID

    }
}
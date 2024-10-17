﻿namespace GameLogic;

public class TicTacTwoBrain
{
    private EGamePiece[,] _gameBoard;
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    public TicTacTwoBrain(int boardSize) : this(boardSize, boardSize)
    {
        
    }
    
    private TicTacTwoBrain(int boardX, int boardY)
    {
        _gameBoard = new EGamePiece[boardX, boardY];
    }

    public EGamePiece[,] GameBoard
    {
        get => GetBoard();
        private set => _gameBoard = value;
    }

    private EGamePiece[,] GetBoard()
    {
        var copyOfBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];

        for (var x = 0; x < _gameBoard.GetLength(0); x++)
        {
            for (var y = 0; y < _gameBoard.GetLength(1); y++)
            {
                copyOfBoard[x, y] = _gameBoard[x, y];
            }
            
        }
        return copyOfBoard;
    }

    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y] != EGamePiece.Empty)
        {
            return false;
        }
        _gameBoard[x, y] = _nextMoveBy;
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return true;
    }
}
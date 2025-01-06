using Domain;

namespace GameLogic;

public class TicTacTwoAi(TicTacTwoBrain brain, EGamePiece piece, EGamePiece opponentPiece)
{
    private TicTacTwoBrain Brain { get; set; } = brain;
    public EGamePiece Piece { get; set; } = piece;
    private EGamePiece OpponentPiece { get; set; } = opponentPiece;
    private Random Random { get; set; } = new();
    private List<(int x, int y)> AiPieces { get; set; } = [];

    public void MakeAMove()
    {
        InitializeAiPieces();
        if (Brain.IsGameOver()) return;
        if (Brain.GetGameState().MovesMade / 2 < Brain.GetGameState().GetGameConfiguration().MovePieceAfterNMoves)
        {
            if (!WinIfPossible() && !BlockOpponentIfPossible())
            {
                MakeRandomMove();
            }
        }
        else
        {
            MakeStrategicMove();
        }
    }

    private bool WinIfPossible(bool movingPiece = false)
    {
        if (movingPiece)
        {
            return WinByMovingPiece();
        }

        foreach (var (x, y) in GetAllEmptySpotsInGrid())
        {
            if (Brain.GetGameState().GameBoard[x][y] != EGamePiece.Empty) continue;
            SimulateMove(x, y);
            if (Brain.CheckForWinner(Piece))
            {
                UndoMove(x, y);
                Brain.MakeAMove(x, y);
                AiPieces.Add((x, y));
                Console.WriteLine("AI wins!");
                return true;
            }
            UndoMove(x, y);
        }
        return false;
    }

    private bool BlockOpponentIfPossible(bool movingPiece = false)
    {
        if (movingPiece)
        {
            return BlockByMovingPiece();
        }

        foreach (var (x, y) in GetAllEmptySpotsInGrid())
        {
            if (Brain.GetGameState().GameBoard[x][y] != EGamePiece.Empty) continue;
            SimulateOpponentMove(x, y);
            if (Brain.CheckForWinner(OpponentPiece))
            {
                UndoMove(x, y);
                Brain.MakeAMove(x, y);
                AiPieces.Add((x, y));
                Console.WriteLine("AI blocks!");
                return true;
            }
            UndoMove(x, y);
        }
        return false;
    }

    private void MakeRandomMove()
    {
        do
        {
            var x = Random.Next(Brain.GridStartX, Brain.GridEndX + 1);
            var y = Random.Next(Brain.GridStartY, Brain.GridEndY + 1);
            if (Brain.GetGameState().GameBoard[x][y] != EGamePiece.Empty) continue;
            Brain.MakeAMove(x, y);
            AiPieces.Add((x, y));
            break;
        } while (true);
    }

    private void RandomMovePiece()
    {
        var validMoves = new List<(int fromX, int fromY, int toX, int toY)>();

        foreach (var (x, y) in AiPieces)
        {
            foreach (var (targetX, targetY) in GetAllEmptySpotsInGrid())
            {
                if (Brain.GetGameState().GameBoard[targetX][targetY] == EGamePiece.Empty)
                {
                    validMoves.Add((x, y, targetX, targetY));
                }
            }
        }

        if (validMoves.Count == 0) return;

        var selectedMove = validMoves[Random.Next(validMoves.Count)];

        Brain.MovePiece(selectedMove.fromX, selectedMove.fromY, selectedMove.toX, selectedMove.toY);
        UpdateAiPieces(selectedMove.fromX, selectedMove.fromY, selectedMove.toX, selectedMove.toY);

        Console.WriteLine($"AI randomly moved a piece from ({selectedMove.fromX}, {selectedMove.fromY}) to ({selectedMove.toX}, {selectedMove.toY})");
    }

    private void MakeStrategicMove()
    {
        if (WinIfPossible(true) || BlockOpponentIfPossible(true)) return;
        var move = Random.Next(1, 3);
        if (move == 1)
        {
            RandomMovePiece();
        }
        else
        {
            RandomGridMove();
        }
    }

    private bool WinByMovingPiece()
    {
        foreach (var (x, y) in AiPieces)
        {
            foreach (var (targetX, targetY) in GetAllEmptySpotsInGrid())
            {
                if (Brain.GetGameState().GameBoard[targetX][targetY] != EGamePiece.Empty) continue;
                
                SimulatePieceMove(x, y, targetX, targetY);

                if (Brain.CheckForWinner(Piece))
                {
                    UndoPieceMove(x, y, targetX, targetY);
                    Brain.MovePiece(x, y, targetX, targetY);
                    UpdateAiPieces(x, y, targetX, targetY);
                    Console.WriteLine("AI wins by moving a piece!");
                    return true;
                }

                UndoPieceMove(x, y, targetX, targetY);
            }
        }

        return false;
    }

    private bool BlockByMovingPiece()
    {
        foreach (var (x, y) in GetAllEmptySpotsInGrid())
        {
            SimulateOpponentMove(x, y);
            
            if (Brain.CheckForWinner(OpponentPiece))
            {
                UndoMove(x, y);
                
                foreach (var (fromX, fromY) in AiPieces)
                {
                    Brain.MovePiece(fromX, fromY, x, y);
                    UpdateAiPieces(fromX, fromY, x, y);
                    Console.WriteLine($"AI blocked the opponent's win by moving a piece from ({fromX}, {fromY}) to ({x}, {y})");
                    return true;
                }
            }
            UndoMove(x, y);
        }

        return false;
    }

    private void RandomGridMove()
    {
        string direction;
        List<string> directions = ["up", "down", "left", "right", "up-left", "up-right", "down-left", "down-right"];
        do
        {
            direction = directions[Random.Next(0, directions.Count)];
        } while (!Brain.MoveGrid(direction));
    }

    private void UpdateAiPieces(int fromX, int fromY, int toX, int toY)
    {
        AiPieces.Remove((fromX, fromY));
        AiPieces.Add((toX, toY));
    }

    private void SimulateMove(int x, int y)
    {
        Brain.GetGameState().GameBoard[x][y] = Piece;
    }

    private void UndoMove(int x, int y)
    {
        Brain.GetGameState().GameBoard[x][y] = EGamePiece.Empty;
    }

    private void SimulateOpponentMove(int x, int y)
    {
        Brain.GetGameState().GameBoard[x][y] = OpponentPiece;
    }
    
    private void SimulatePieceMove(int fromX, int fromY, int toX, int toY)
    {
        Brain.GetGameState().GameBoard[toX][toY] = Brain.GetGameState().GameBoard[fromX][fromY];
        Brain.GetGameState().GameBoard[fromX][fromY] = EGamePiece.Empty;
    }
    
    private void UndoPieceMove(int fromX, int fromY, int toX, int toY)
    {
        Brain.GetGameState().GameBoard[fromX][fromY] = Brain.GetGameState().GameBoard[toX][toY];
        Brain.GetGameState().GameBoard[toX][toY] = EGamePiece.Empty;
    }
    
    private IEnumerable<(int x, int y)> GetAllEmptySpotsInGrid()
    {
        for (var y = Brain.GridStartY; y <= Brain.GridEndY; y++)
        {
            for (var x = Brain.GridStartX; x <= Brain.GridEndX; x++)
            {
                if (Brain.GetGameState().GameBoard[x][y] == EGamePiece.Empty)
                {
                    yield return (x, y);
                }
            }
        }
    }
    
    private void InitializeAiPieces()
    {
        AiPieces.Clear();
        for (var y = 0; y < Brain.GetGameState().GameBoard.Length; y++)
        {
            for (var x = 0; x < Brain.GetGameState().GameBoard[y].Length; x++)
            {
                if (Brain.GetGameState().GameBoard[x][y] == Piece)
                {
                    AiPieces.Add((x, y));
                }
            }
        }
        
    }
}
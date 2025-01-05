namespace Domain;

public static class ConfigurationConverter
{
    public static DbConfiguration ToTicTacTwoConfiguration(GameConfiguration gameConfig)
    {
        return new DbConfiguration
        {
            ConfigurationName = gameConfig.Name,
            BoardSizeWidth = gameConfig.BoardSizeWidth,
            BoardSizeHeight = gameConfig.BoardSizeHeight,
            GridSizeWidth = gameConfig.GridSizeWidth,
            GridSizeHeight = gameConfig.GridSizeHeight,
            WinCondition = gameConfig.WinCondition,
            MovePieceAfterNMoves = gameConfig.MovePieceAfterNMoves
        };
    }

    public static GameConfiguration ToGameConfiguration(DbConfiguration dbConfig)
    {
        return new GameConfiguration
        {
            Name = dbConfig.ConfigurationName,
            BoardSizeWidth = dbConfig.BoardSizeWidth,
            BoardSizeHeight = dbConfig.BoardSizeHeight,
            GridSizeWidth = dbConfig.GridSizeWidth,
            GridSizeHeight = dbConfig.GridSizeHeight,
            WinCondition = dbConfig.WinCondition,
            MovePieceAfterNMoves = dbConfig.MovePieceAfterNMoves
        };
    }
}
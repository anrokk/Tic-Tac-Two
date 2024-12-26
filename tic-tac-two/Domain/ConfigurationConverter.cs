namespace Domain;

public static class ConfigurationConverter
{
    public static DbConfiguration ToTicTacTwoConfiguration(GameConfiguration gameConfiguration)
    {
        return new DbConfiguration
        {
            ConfigurationName = gameConfiguration.Name,
            BoardSizeWidth = gameConfiguration.BoardSizeWidth,
            BoardSizeHeight = gameConfiguration.BoardSizeHeight,
            GridSizeWidth = gameConfiguration.GridSizeWidth,
            GridSizeHeight = gameConfiguration.GridSizeHeight,
            WinCondition = gameConfiguration.WinCondition,
            MovePieceAfterNMoves = gameConfiguration.MovePieceAfterNMoves
        };
    }
    
    public static GameConfiguration ToGameConfiguration(DbConfiguration dbConfiguration)
    {
        return new GameConfiguration
        {
            Name = dbConfiguration.ConfigurationName,
            BoardSizeWidth = dbConfiguration.BoardSizeWidth,
            BoardSizeHeight = dbConfiguration.BoardSizeHeight,
            GridSizeWidth = dbConfiguration.GridSizeWidth,
            GridSizeHeight = dbConfiguration.GridSizeHeight,
            WinCondition = dbConfiguration.WinCondition,
            MovePieceAfterNMoves = dbConfiguration.MovePieceAfterNMoves
        };
    }
}
namespace DAL;

public class FileHelper
{
    public const string GameExtension = ".game.json";
    public const string ConfigExtension = ".config.json";
    public static readonly string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                             + Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;
}
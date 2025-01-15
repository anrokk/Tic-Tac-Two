namespace DAL;

public static class FileHelper
{
    public static readonly string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                             + Path.DirectorySeparatorChar + "Recipe" + Path.DirectorySeparatorChar;

}
using GameLogic;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{

    private readonly string _basePath = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                                        Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;
    
    public List<string> GetConfigurationNames()
    {
        CheckAndCreateConfig();
        
        return System.IO.Directory.GetFiles(FileHelper.BasePath,"*" + FileHelper.ConfigExtension).ToList();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        throw new NotImplementedException();
    }

    private void CheckAndCreateConfig()
    {
        if (!Directory.Exists(FileHelper.BasePath))
        {
            Directory.CreateDirectory(FileHelper.BasePath);
        }

        var data = System.IO.Directory.GetFiles(FileHelper.BasePath, "*" + FileHelper.ConfigExtension).ToList();
        
        if (data.Count == 0)
        {
            var hardcodedRepo = new ConfigRepositoryHardcoded();
            var optionNames = hardcodedRepo.GetConfigurationNames();
            foreach (var optionName in optionNames)
            {
                var gameOption = hardcodedRepo.GetConfigurationByName(optionName);
                var optionJsonStr = System.Text.Json.JsonSerializer.Serialize(gameOption);
                File.WriteAllText(FileHelper.BasePath + gameOption.Name + FileHelper.ConfigExtension, optionJsonStr);
            }
        }
    }
}
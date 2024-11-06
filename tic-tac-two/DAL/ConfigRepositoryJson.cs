using System.Text.Json;
using GameLogic;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{
    public List<string> GetConfigurationNames()
    {
        CheckAndCreateConfig();
        
        return Directory
            .GetFiles(FileHelper.BasePath, "*" + FileHelper.ConfigExtension)
            .Select(fullFileName => 
                Path.GetFileNameWithoutExtension(
                    Path.GetFileNameWithoutExtension(fullFileName)
                )
            )
            .ToList();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        var configJsonStr = File.ReadAllText(FileHelper.BasePath + name + FileHelper.ConfigExtension);
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        
        return config;  
        // TODO check for possible errors, scenario: maybe if there is no config with such name
    }

    public void SaveConfiguration(GameConfiguration config)
    {
        var optionJsonStr = JsonSerializer.Serialize(config);
        File.WriteAllText(FileHelper.BasePath + config.Name + FileHelper.ConfigExtension, optionJsonStr);
    }
    

    private static void CheckAndCreateConfig()
    {
        if (!Directory.Exists(FileHelper.BasePath))
        {
            Directory.CreateDirectory(FileHelper.BasePath);
        }
        
        var hardcodedRepo = new ConfigRepositoryHardcoded();
        var optionNames = hardcodedRepo.GetConfigurationNames();
        
        foreach (var optionName in optionNames)
        {
            var gameOption = hardcodedRepo.GetConfigurationByName(optionName);
            var optionJsonStr = JsonSerializer.Serialize(gameOption);
            File.WriteAllText(FileHelper.BasePath + gameOption.Name + FileHelper.ConfigExtension, optionJsonStr);
        }
        
    }
}
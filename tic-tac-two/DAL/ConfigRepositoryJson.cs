using System.Text.Json;
using Domain;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository

{
    public List<string> GetConfigurationNames(string username)
    {
        CheckAndCreateConfig(username);
        var configFiles = Directory.GetFiles(FileHelper.BasePath, $"*_{username}{FileHelper.ConfigExtension}").ToList();

        return configFiles.Select(Path.GetFileNameWithoutExtension)
            .Select(name => name!.Split('_')[0])
            .ToList();
    }

    public GameConfiguration GetConfiguration(string name, string username)
    {
        var filePath = FileHelper.BasePath + name + "_" + username + FileHelper.ConfigExtension;

        if (!File.Exists(filePath))
        {
            throw new InvalidOperationException($"Configuration {name} for user {username} not found.");
        }

        var configJsonStr = File.ReadAllText(filePath);
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        return config ?? throw new InvalidOperationException("Configuration not found.");
    }

    public List<GameConfiguration> GetAllConfigurations(string username)
    {
        CheckAndCreateConfig(username);
        var configFiles = Directory.GetFiles(FileHelper.BasePath, $"*_{username}{FileHelper.ConfigExtension}").ToList();

        return configFiles
            .Select(configFile =>
            {
                try
                {
                    var jsonContent = File.ReadAllText(configFile);
                    return JsonSerializer.Deserialize<GameConfiguration>(jsonContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to deserialize file {configFile}: {ex.Message}");
                    return null;
                }
            })
            .Where(config => config != null && config.Username == username)
            .ToList()!;
    }

    public void SaveConfiguration(GameConfiguration config, string username)
    {
        config.Username = username;

        var filePath = FileHelper.BasePath + config.Name + "_" + username + FileHelper.ConfigExtension;
        var optionJsonStr = JsonSerializer.Serialize(config);
        File.WriteAllText(filePath, optionJsonStr);
    }

    public void DeleteConfiguration(GameConfiguration config, string username)
    {
        var filePath = FileHelper.BasePath + config.Name + "_" + username + FileHelper.ConfigExtension;

        if (!File.Exists(filePath))
        {
            throw new InvalidOperationException($"Configuration {config.Name} for user {username} not found.");
        }

        File.Delete(filePath);
    }

    private static void CheckAndCreateConfig(string username)
    {
        if (!Directory.Exists(FileHelper.BasePath))
        {
            Directory.CreateDirectory(FileHelper.BasePath);
        }

        var hardCodedRepo = new ConfigRepositoryHardcoded();
        var optionNames = hardCodedRepo.GetConfigurationNames(username);

        foreach (var optionName in optionNames)
        {
            var gameOption = hardCodedRepo.GetConfiguration(optionName, username);
            gameOption.Username = username; // Associate the username with the configuration
            var optionJsonStr = JsonSerializer.Serialize(gameOption);
            File.WriteAllText(FileHelper.BasePath + gameOption.Name + "_" + username + FileHelper.ConfigExtension, optionJsonStr);
        }
    }
}
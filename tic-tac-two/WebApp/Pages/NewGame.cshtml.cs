using DAL;
using Domain;
using GameLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class NewGame(IConfigRepository configRepository, IGameRepository gameRepository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }

    [BindProperty] 
    public string ConfigName { get; set; } = null!;
    
    [BindProperty]
    public EGameMode GameMode { get; set; }

    public List<GameConfiguration> Configurations { get; set; } = null!;

    public TicTacTwoBrain GameInstance { get; set; } = null!;
    
    public IActionResult OnGet()
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        Configurations = configRepository.GetAllConfigurations(Username!);

        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("/Index", new { ErrorMessage = "Username is required to play." });
        }

        return Page();
    }

    public IActionResult OnPostCreateGame()
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;

        Configurations = configRepository.GetAllConfigurations(Username!);

        if (string.IsNullOrEmpty(ConfigName))
        {
            TempData["ErrorMessage"] = "Configuration is required to play!";
        }

        var configuration = Configurations.FirstOrDefault(c => c.Name == ConfigName);

        if (configuration == null)
        {
            TempData["ErrorMessage"] = "Configuration was not found.";
            return Page();
        }

        GameInstance = new TicTacTwoBrain(configuration);
        gameRepository.SaveGame(GameInstance.GetGameState(), Username!);

        var gameId = GameInstance.GetGameState().GetGameId();

        if (GameMode == EGameMode.AivsAi)
        {
            return RedirectToPage("/Game", new
            {
                gameid = gameId,
                gamemode = GameMode,
                player = EGamePiece.Empty
            });
        }
        return RedirectToPage("/Game", new
        {
            gameid = gameId,
            gamemode = GameMode,
            player = EGamePiece.X
        });
    }
}
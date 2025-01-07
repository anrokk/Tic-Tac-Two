using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class LoadGame(IGameRepository gameRepo) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }

    public List<GameState> GamesList { get; set; } = null!;
    [BindProperty]
    public EGameMode GameMode { get; set; }
    [BindProperty]
    public string? GameId { get; set; }

    public IActionResult OnGet(string? username)
    {
        Username = UsernameHelper.GetUsername(HttpContext, username)!;

        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("/Index", new { ErrorMessage = "Username is required." });
        }

        GamesList = gameRepo.GetAllGameStates(Username);
        
        return Page();
    }

    public IActionResult OnPostLoadTheGame()
    {
        if (string.IsNullOrEmpty(GameId))
        {
            TempData["ErrorMessage"] = "Game is required!";
            return Page();
        }

        if (GameMode == EGameMode.AivsAi)
        {
            return RedirectToPage("/Game", new { 
                gameid = GameId, 
                gamemode = GameMode, 
                player = EGamePiece.Empty });
        }
        return RedirectToPage("/Game", new { 
            gameid = GameId, 
            gamemode = GameMode, 
            player = EGamePiece.X });
    }
}
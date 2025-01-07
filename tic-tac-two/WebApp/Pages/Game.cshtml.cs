using DAL;
using Domain;
using GameLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Game(IGameRepository gameRepo, GameService gameService, GameContext context) : PageModel
{
    [BindProperty(SupportsGet = true)] public string? Username { get; set; }
    public string? GameId { get; set; }
    public EGameMode? GameMode { get; set; }
    public EGamePiece? Player { get; set; }
    public GameConfiguration Configuration { get; set; } = null!;
    public TicTacTwoBrain GameInstance { get; set; } = null!;
    public GameState GameState { get; set; } = null!;
    public GameContext Context { get; set; } = context;
    public string Message { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;

    public IActionResult OnGet(string gameId, EGameMode gameMode, EGamePiece player, int? x, int? y, string? move,
        string? direction, int? selectedX, int? selectedY)
    {
        ValidateUsername();

        SetGameProperties(gameId, gameMode, player);

        SetContextProperties(direction, selectedX, selectedY);

        var result = gameMode switch
        {
            EGameMode.PvsAi => gameService.PlayPvsAi(GameInstance, x, y, move, Username!),
            EGameMode.PvsP => gameService.PlayPvsP(GameInstance, x, y, move, Username!),
            EGameMode.AivsAi => gameService.PlayAivsAi(GameInstance, Username!),
            _ => (Success: false, Message: "Game mode is invalid.")
        };

        if (!result.Success)
        {
            ErrorMessage = result.Message;
            return Page();
        }

        Message = result.Message;
        return Page();
    }

    private void SetGameProperties(string gameId, EGameMode gameMode, EGamePiece player)
    {
        GameId = gameId;
        GameMode = gameMode;
        Player = player;

        GameState = gameRepo.LoadGame(GameId, Username!);
        Configuration = GameState.GetGameConfiguration();
        GameInstance = new TicTacTwoBrain(GameState);
    }

    private void SetContextProperties(string? direction, int? selectedX, int? selectedY)
    {
        Context.Direction = direction;
        Context.SelectedX = selectedX;
        Context.SelectedY = selectedY;
    }

    private IActionResult ValidateUsername()
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;

        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("/Index", new { ErrorMessage = "Username is required." });
        }

        return Page();
    }
}
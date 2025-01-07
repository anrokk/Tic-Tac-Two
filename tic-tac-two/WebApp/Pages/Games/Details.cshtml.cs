using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Games;

public class DetailsModel(IGameRepository repository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }
    
    public GameState SaveGame { get; set; } = null!;
    
    public IActionResult OnGet(string? id)
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var saveGame = repository.LoadGame(id, Username!);

        SaveGame = saveGame;

        return Page();
    }
}
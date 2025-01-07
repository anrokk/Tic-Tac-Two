using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Games; 

public class DeleteModel(IGameRepository repository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }
    
    [BindProperty]
    public GameState SaveGame { get; set; } = null!;
    
    public IActionResult OnGet(string? id)
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var saveGame = repository.LoadGame(id, Username);

        SaveGame = saveGame;

        return Page();
    }

    public IActionResult OnPost(string? id)
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username!);
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        
        repository.DeleteGame(id, Username!);

        return RedirectToPage("./Index");
    }
}
using DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages.Games
{
    public class IndexModel(IGameRepository repository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Username { get; set; }
        public IList<GameState> SaveGame { get;set; } = null!;

        public Task OnGet()
        {
            Username = UsernameHelper.GetUsername(HttpContext, Username)!;
            SaveGame = repository.GetAllGameStates(Username!);
            return Task.CompletedTask;
        }
    }
}
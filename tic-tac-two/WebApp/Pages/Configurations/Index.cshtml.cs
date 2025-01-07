using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Configurations;

public class IndexModel(IConfigRepository repository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }

    public IList<GameConfiguration> Configurations { get; set; } = null!;
    
    public Task OnGet()
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        Configurations = repository.GetAllConfigurations(Username);
        return Task.CompletedTask;
    }
}
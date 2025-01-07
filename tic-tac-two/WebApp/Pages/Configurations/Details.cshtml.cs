using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Configurations;

public class DetailsModel(IConfigRepository repository) : PageModel
{
    
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }
    
    public GameConfiguration Configuration { get; set; } = null!;
    
    public IActionResult OnGet(string? name)
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        if (string.IsNullOrEmpty(name))
        {
            return NotFound();
        }
        
        var configuration = repository.GetConfiguration(name, Username);
        Configuration = configuration;
        return Page();
    }
}
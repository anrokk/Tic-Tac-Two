using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Configurations;

public class EditModel(IConfigRepository repository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }
    
    [BindProperty]
    public GameConfiguration Configuration { get; set; } = null!;
    
    public IActionResult OnGet(string? name)
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        if (name == null)
        {
            return NotFound();
        }

        var gameConfiguration = repository.GetConfiguration(name, Username);
        Configuration = gameConfiguration;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        repository.SaveConfiguration(Configuration, Username!);

        return RedirectToPage("./Index");
    }
}
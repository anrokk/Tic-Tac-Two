using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Configurations;

public class DeleteModel(IConfigRepository repository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }
    
    [BindProperty]
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

    public IActionResult OnPost(string? name)
    {
        Username = UsernameHelper.GetUsername(HttpContext, Username)!;
        if (string.IsNullOrEmpty(name))
        {
            return NotFound();
        }
        var configuration = repository.GetConfiguration(name, Username!);
        Configuration = configuration;
        repository.DeleteConfiguration(configuration, Username!);

        return RedirectToPage("./Index");
    }
}
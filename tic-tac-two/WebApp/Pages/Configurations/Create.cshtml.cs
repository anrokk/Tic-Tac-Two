using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;

namespace WebApp.Pages.Configurations
{
    public class CreateModel(IConfigRepository repository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Username { get; set; }
        
        public IActionResult OnGet()
        {
            Username = UsernameHelper.GetUsername(HttpContext, Username)!;

            return Page();
        }

        [BindProperty]
        public GameConfiguration Configuration { get; set; } = null!;

        public IActionResult OnPost()
        {
            Username = UsernameHelper.GetUsername(HttpContext, Username)!;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            repository.SaveConfiguration(Configuration, Username!);

            return RedirectToPage("./Index");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string? Username { get; set; }
    public string ErrorMessage { get; set; } = null!;

    public IActionResult OnGet(string? error)
    {
        if (error != null)
        {
            ErrorMessage = error;
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            ErrorMessage = "Username is required";
            return Page();
        }

        Username = Username.Trim();
        HttpContext.Session.SetString("Username", Username);

        return RedirectToPage("/home");
    }
}
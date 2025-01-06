using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Home : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Username { get; set; } = null!;
    
    public IActionResult OnGet(string? username)
    {
        ViewData["Username"] = username;
        Username = UsernameHelper.GetUsername(HttpContext, username)!;

        if (string.IsNullOrEmpty(Username))
        {
            return RedirectToPage("/Index", new { ErrorMessage = "Username is required" });
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        return Page();
    }

    public IActionResult OnPostNewGame()
    {
        return RedirectToPage("/NewGame");
    }
    
    public IActionResult OnPostLoadGame()
    {
        return RedirectToPage("/LoadGame");
    }

    public IActionResult OnPostOptions()
    {
        return RedirectToPage("/Options");
    }
}
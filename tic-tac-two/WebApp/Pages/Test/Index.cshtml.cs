using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Test;

public class Index : PageModel
{
    [BindProperty] 
    public string Name { get; set; } = default!;
    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        
    }
}
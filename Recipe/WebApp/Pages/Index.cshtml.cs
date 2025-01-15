using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    private readonly AppDbContext _context;
    
    [BindProperty] public string Search { get; set; } = default!;

    [BindProperty] public List<Recipe> Recipes { get; set; } = [];

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(Search))
        {
            return Page();
        }
        
        Search = Search.Trim().ToLower();
        Recipes = _context.Recipes
            .Include(i => i.IngredientsInRecipe!)
            .ThenInclude(j => j.Ingredient)
            .Where(r => 
                r.RecipeName.ToLower().Contains(Search) || 
                r.Description.ToLower().Contains(Search) ||
                r.IngredientsInRecipe!.Any(i => i.Ingredient!.IngredientName.ToLower().Contains(Search))
            )
            .ToList();
        
        return Page();
    }
}
namespace Domain;

public class IngredientInRecipe : BaseEntity
{
    public decimal Amount { get; set; } // ei pea alati olema täisarv
    
    //fk
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    //fk
    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
}
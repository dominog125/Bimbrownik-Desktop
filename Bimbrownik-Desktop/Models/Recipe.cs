namespace Bimbrownik_Desktop.Models;

public class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Ingredients { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string? Author { get; set; }
    public Guid AlcoholCategoryId { get; set; }
    public string? AlcoholCategoryName { get; set; }

    public bool IsHighlighted { get; set; }
}
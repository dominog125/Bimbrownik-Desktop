namespace Bimbrownik_Desktop.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public bool IsHighlighted { get; set; } 
        public int ViewCount { get; set; }
        public DateTime? LastViewedAt { get; set; }
    }
}
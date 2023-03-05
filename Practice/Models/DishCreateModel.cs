using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Practice.Models;

public class DishCreateModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<IngredientCreateModel>? Ingredients { get; set; }


}

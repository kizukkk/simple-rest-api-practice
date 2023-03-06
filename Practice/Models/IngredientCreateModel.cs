using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Practice.Models;

public class IngredientCreateModel
{
    [NotNull]
    [MinLength(3)]
    public string? Name { get; set; }

    [NotNull]
    [MinLength(6)]
    public string? Description { get; set; }


}

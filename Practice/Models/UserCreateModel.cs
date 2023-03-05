using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Practice.Models;

public class UserCreateModel
{
    public string? Name { get; set; }

    [NotNull]
    [MinLength(6)]
    public string? Email { get; set; }

    [NotNull]
    [MinLength(8)]
    public string? Password { get; set; }

}

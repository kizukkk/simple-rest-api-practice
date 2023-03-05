using Microsoft.EntityFrameworkCore;


namespace Practice.Entity;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public virtual ICollection<Dish>? Dishes { get; set;}
}

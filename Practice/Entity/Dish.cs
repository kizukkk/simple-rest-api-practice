namespace Practice.Entity;

public class Dish
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Ingredient>? Ingredients { get; set; }
    public virtual ICollection<User>? Users { get; set; }


}
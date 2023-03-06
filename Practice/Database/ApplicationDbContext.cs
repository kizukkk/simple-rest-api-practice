using Microsoft.EntityFrameworkCore;
using Practice.Entity;

namespace Practice.Database;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) :
        base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<User> Users { get; set; }
}

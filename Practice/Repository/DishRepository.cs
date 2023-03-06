using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Database;
using Practice.Entity;
using Practice.Models;

namespace Practice.Repository;

public class DishRepository : IDishRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DishRepository(ApplicationDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

    public async Task<ICollection<DishViewModel>> ShowAll()
    {
        var list = _context.Dishes
            .Include(i => i.Ingredients)
            .ToListAsync();

        return _mapper.Map<List<DishViewModel>>(list.Result);

    }

    public async Task<DishViewModel> ShowOne(int id)
    {
        var dish = await _context.Dishes
            .Include(i => i.Ingredients)
            .FirstOrDefaultAsync(d => d.Id == id);


        return _mapper.Map<DishViewModel>(dish);
    }

    public async Task<DishViewModel> Create(DishCreateModel newDish)
    {
        var dishToDb = _mapper.Map<Dish>(newDish);
        var dd = _context.Dishes.Where(d => d.Name == dishToDb.Name);

        if (!_context.Dishes.Where(d => d.Name == dishToDb.Name).IsNullOrEmpty())
            throw new Exception("A dish with this name already exists");

        if (!dishToDb.Ingredients.IsNullOrEmpty())
        {
            foreach (var ingr in dishToDb.Ingredients!.ToList())
            {
                var ing_db = _context.Ingredients.Where(i => i.Name == ingr.Name);
                if (ing_db != null)
                {
                    dishToDb.Ingredients.Remove(ingr);
                    dishToDb.Ingredients.Add(ing_db.First());
                }
            }
        }


        await _context.Dishes.AddAsync(dishToDb);
        await _context.SaveChangesAsync();

        return _mapper.Map<DishViewModel>(dishToDb);

    }

    public Task<IActionResult> Delete(int id)
    {
        throw new NotImplementedException();
    }

}

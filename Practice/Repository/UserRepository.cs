using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Database;
using Practice.Entity;
using Practice.Models;

namespace Practice.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext context, IMapper mapper) => 
        (_context, _mapper) = (context, mapper);


    public async Task<ICollection<UserViewModel>> ShowAll()
    {
        var list = await _context.Users.ToListAsync();

        return _mapper.Map<List<UserViewModel>>(list);

    }
    public async Task<UserViewModel> ShowOne(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return _mapper.Map<UserViewModel>(user);
    }
    public async Task<ICollection<DishViewModel>> GetDishList(int id)
    {
        var user = await _context.Users.FindAsync(id) 
            ?? throw new Exception("A user with that id is not exist");
        var dish = _context.Dishes.Where(d => d.Users!.Contains(user));


        if (user.Dishes.IsNullOrEmpty() && dish.IsNullOrEmpty())
            throw new Exception("A user with this id does not have a list of dishes");

        var dishList = _context.Dishes
            .Include(u => u.Users)
            .Include(i => i.Ingredients)
            .Where(d => d.Users!.Contains(user)).ToList();

        return _mapper.Map<List<DishViewModel>>(dishList);
    }
    public async Task<DishViewModel> AddDishToList(int user_id, int dish_id)
    {
        var user = await _context.Users.FindAsync(user_id);
        var dish = await _context.Dishes
            .Include(d => d.Ingredients)
            .FirstAsync(d => d.Id == dish_id);

        if (user is null)
            throw new Exception("A user with this id not exists");
        if (dish is null)
            throw new Exception("A dish with this id not exists");

        if (user.Dishes is null)
            user.Dishes = new List<Dish>();

        user.Dishes.Add(dish);

        await _context.SaveChangesAsync();

        return _mapper.Map<DishViewModel>(dish);


    }
    public async Task<UserViewModel> Create(UserCreateModel newUser)
    {

        var checkEmail =
            _context.Users.Where(u => u.Email == newUser.Email);

        if (!checkEmail.IsNullOrEmpty())
            throw new Exception("a user with that email already exists");


        var userToDB = _mapper.Map<User>(newUser);

        _context.Users.Add(userToDB);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserViewModel>(userToDB);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users
            .Include(u => u.Dishes)
            .FirstAsync(u => u.Id == id)
            ?? throw new Exception("A user with that id is not exist");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return new OkResult();

    }

    public Task<IActionResult> DeleteDishFromList(int user_id, int dish_id)
    {
        throw new NotImplementedException();
    }

}

using Microsoft.AspNetCore.Mvc;
using Practice.Models;

namespace Practice.Repository;


public interface IUserRepository
{
    public Task<ICollection<UserViewModel>> ShowAll();
    public Task<UserViewModel> ShowOne(int id);
    public Task<UserViewModel> Create(UserCreateModel newUser);
    public Task<IActionResult> Delete(int id);
    public Task<ICollection<DishViewModel>> GetDishList(int id);
    public Task<DishViewModel> AddDishToList(int user_id, int dish_id);
    public Task<IActionResult> DeleteDishFromList(int user_id, int dish_id);


}

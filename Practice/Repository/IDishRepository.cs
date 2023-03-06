using Microsoft.AspNetCore.Mvc;
using Practice.Models;

namespace Practice.Repository;


public interface IDishRepository
{
    public Task<ICollection<DishViewModel>> ShowAll();
    public Task<DishViewModel> ShowOne(int id);
    public Task<DishViewModel> Create(DishCreateModel newDish);
    public Task<IActionResult> Delete(int id);
}

using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Repository;


namespace Practice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UsersController(IUserRepository repo) =>
        _repo = repo;

    [HttpGet]
    public async Task<ActionResult<ICollection<UserViewModel>>> ShowAll()
    {
        var list = await _repo.ShowAll();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ICollection<UserViewModel>>> ShowOne(int id)
    {
        var user = await _repo.ShowOne(id);

        if (user is null)
            return NotFound("A user with that id is not exist");

        return Ok(user);
    }


    [HttpPost]
    public async Task<ActionResult<UserViewModel>> Create(
        [FromBody] UserCreateModel user)
    {
        UserViewModel? newUser;

        try
        {
            newUser = await _repo.Create(user);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }


        return Created($"api/users/{newUser.Id}", newUser);
    }

    
    [HttpPost]
    [Route("{user_id}/dishes/{dish_id}")]
    public async Task<ActionResult<UserViewModel>> AddDishToList(
         int user_id,
         int dish_id)
    {
        DishViewModel? addedDish = null;

        try
        {
            addedDish = await _repo.AddDishToList(user_id, dish_id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


        return Created($"api/users/{user_id}/dishes/{dish_id}", addedDish);
    }

    
    [HttpGet("{user_id}/dishes")]
    public async Task<ActionResult<ICollection<DishViewModel>>> GetDishList(int user_id)
    {
        ICollection<DishViewModel>? listOfDish;

        try
        {
            listOfDish = await _repo.GetDishList(user_id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(listOfDish);
    }

    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _repo.Delete(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }
}

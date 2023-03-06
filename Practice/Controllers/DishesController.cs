using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Repository;

namespace Practice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishesController : ControllerBase
{
    private readonly IDishRepository _repo;

    public DishesController(IDishRepository repository) => 
        _repo = repository;

    [HttpGet]
    public async Task<ActionResult<ICollection<DishViewModel>>> ShowAll()
    {
        var list = await _repo.ShowAll();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ICollection<DishViewModel>>> ShowOne(int id)
    {
        var dish = await _repo.ShowOne(id);

        if(dish is null)
            return NotFound();

        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<DishViewModel>> Create(DishCreateModel dish)
    {
        DishViewModel? newDish = null;
        try
        {
            newDish = await _repo.Create(dish);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Created("api/dishes/1", newDish);
    }


}

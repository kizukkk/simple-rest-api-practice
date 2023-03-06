using AutoMapper;
using Practice.Entity;
using Practice.Models;

namespace Practice.Extensions;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserCreateModel, User>();
        CreateMap<User, UserViewModel>();

        CreateMap<IngredientCreateModel, Ingredient>();
        CreateMap<Ingredient, IngredientViewModel>();

        CreateMap<DishCreateModel, Dish>();
        CreateMap<Dish, DishViewModel>();

    }

}

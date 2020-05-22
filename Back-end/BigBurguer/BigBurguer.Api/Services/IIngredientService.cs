using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBurguer.Api.Services
{
    public interface IIngredientService
    {
        public Task<EntityEntry<Ingredient>> CreateIngredientAsync(IngredientViewModel ingredientModel);
        public List<Ingredient> GetAll();
        public Ingredient GetId(int id);
        public bool EditIngredientAsync(int id, IngredientViewModel ingredientModel);
        public bool DeleteIngredientAsync(int id);
    }
}

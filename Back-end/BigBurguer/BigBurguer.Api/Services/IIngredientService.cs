using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBurguer.Api.Services
{
    public interface IIngredientService
    {
        public EntityEntry<Ingredient> CreateIngredient(IngredientViewModel ingredientModel);
        public List<Ingredient> GetAll();
        public Ingredient GetId(int id);
        public bool EditIngredient(int id, IngredientViewModel ingredientModel);
        public bool DeleteIngredient(int id);
    }
}

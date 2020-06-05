using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBurguer.Api.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly AppDbContext _context;

        public IngredientService(AppDbContext context)
        {
            _context = context;
        }

        public List<Ingredient> GetAll()
        {
            return _context.Ingredients.ToList();
        }

        public Ingredient GetId(int id)
        {
            return _context.Ingredients.Where(i => i.Id == id).FirstOrDefault();
        }

        public EntityEntry<Ingredient> CreateIngredient(IngredientViewModel ingredientModel)
        {
            Ingredient ingredient = new Ingredient()
            {
                Name = ingredientModel.Name,
                StockQuantity = ingredientModel.StockQuantity
            };

            var result = _context.Ingredients.Add(ingredient);

            if (result != null)
            {
                _context.SaveChanges();

                return result;
            }
            return null;
        }

        public bool EditIngredient(int id, IngredientViewModel ingredientModel)
        {
            var ingredient = _context.Ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingredient != null)
            {
                ingredient.Name = ingredientModel.Name;
                ingredient.StockQuantity = ingredientModel.StockQuantity;

                _context.Ingredients.Update(ingredient);

                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteIngredient(int id)
        {
            var ingredient = _context.Ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}

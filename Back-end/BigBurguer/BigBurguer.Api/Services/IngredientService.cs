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

        public async Task<EntityEntry<Ingredient>> CreateIngredientAsync(IngredientViewModel ingredientModel)
        {
            Ingredient ingredient = new Ingredient()
            {
                Name = ingredientModel.Name,
                StockQuantity = ingredientModel.StockQuantity
            };

            var result = _context.Ingredients.AddAsync(ingredient);

            if (result != null)
            {
                _context.SaveChanges();

                if (ingredientModel.ProductId != null)
                {
                    var productIngredient = new ProductIngredient()
                    {
                        ProductId = ingredientModel.ProductId,
                        IngredientId = ingredient.Id
                    };

                    AddProductIngredient(productIngredient);
                }

                return await result;
            }
            return null;
        }

        private void AddProductIngredient(ProductIngredient productIngredient)
        {
            _context.ProductIngredients.Add(productIngredient);
            _context.SaveChanges();
        }

        public bool EditIngredientAsync(int id, IngredientViewModel ingredientModel)
        {
            var ingredient = _context.Ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingredient != null)
            {
                ingredient.Name = ingredientModel.Name;
                ingredient.StockQuantity = ingredient.StockQuantity;

                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteIngredientAsync(int id)
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

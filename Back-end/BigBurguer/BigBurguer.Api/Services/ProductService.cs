using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace BigBurguer.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetId(int productId)
        {
            return _context.Products.Find(productId);
        }

        public EntityEntry<Product> CreateProduct(ProductViewModel productModel)
        {
            Product product = new Product()
            {
                ImageUrl = productModel.ImageUrl,
                Name = productModel.Name,
                Price = productModel.Price,
                Type = productModel.Type
            };

            var result = _context.Products.Add(product);

            if (result != null)
            {
                _context.SaveChanges();

                foreach (var ingrId in productModel.Ingredients)
                {
                    ProductIngredient prodIng = new ProductIngredient();
                    prodIng.ProductId = result.Entity.Id;
                    prodIng.IngredientId = ingrId.IngredientId;
                    prodIng.Quantity = ingrId.Quantity;

                    _context.ProductIngredients.Add(prodIng);
                    _context.SaveChanges();
                }

                return result;
            }
            return null;
        }

        public bool EditProduct(int productId, ProductViewModel productModel)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                product.ImageUrl = productModel.ImageUrl;
                product.Name = productModel.Name;
                product.Price = productModel.Price;
                product.Type = productModel.Type;

                _context.Products.Update(product);

                _context.SaveChanges();

                foreach (var ingrId in productModel.Ingredients)
                {
                    ProductIngredient prodIng = new ProductIngredient();
                    prodIng.ProductId = product.Id;
                    prodIng.IngredientId = ingrId.IngredientId;
                    prodIng.Quantity = ingrId.Quantity;

                    _context.ProductIngredients.Update(prodIng);
                    _context.SaveChanges();
                }

                return true;
            }
            return false;
        }

        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.Where(i => i.Id == productId).FirstOrDefault();

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public Product GetIngredientsDetails(int productId)
        {
            return _context.Products
                .Include(i => i.ProductIngredients)
                .FirstOrDefault(pi => pi.Id == productId);
        }
    }
}

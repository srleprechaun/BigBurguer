using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Product GetId(int id)
        {
            return _context.Products.Where(i => i.Id == id).FirstOrDefault();
        }
        public async Task<EntityEntry<Product>> CreateProductAsync(ProductViewModel productModel)
        {
            Product product = new Product()
            {
                ImageUrl = productModel.ImageUrl,
                Name = productModel.Name,
                Price = productModel.Price
            };

            var result = _context.Products.AddAsync(product);

            if (result != null)
            {
                _context.SaveChanges();

                if (productModel.IngredientId != null)
                {
                    var productIngredient = new ProductIngredient()
                    {
                        ProductId = product.Id,
                        IngredientId = productModel.IngredientId
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
        public bool EditProductAsync(int id, ProductViewModel productModel)
        {
            var product = _context.Products.Where(i => i.Id == id).FirstOrDefault();

            if (product != null)
            {
                product.ImageUrl = productModel.ImageUrl;
                product.Name = productModel.Name;
                product.Price = productModel.Price;

                _context.SaveChanges();

                return true;
            }
            return false;
        }
        public bool DeleteProductAsync(int id)
        {
            var product = _context.Products.Where(i => i.Id == id).FirstOrDefault();

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}

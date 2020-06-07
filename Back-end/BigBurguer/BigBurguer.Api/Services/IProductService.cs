using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BigBurguer.Api.Services
{
    public interface IProductService
    {
        public EntityEntry<Product> CreateProduct(ProductViewModel productModel);
        public List<Product> Get();
        public Product GetId(int productId);
        public bool EditProduct(int id, ProductViewModel productModel);
        public bool DeleteProduct(int productId);
        public List<ProductIngredient> GetIngredientsDetails(int productId);
    }
}

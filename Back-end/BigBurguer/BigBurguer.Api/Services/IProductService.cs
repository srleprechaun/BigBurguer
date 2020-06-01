using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBurguer.Api.Services
{
    public interface IProductService
    {
        public Task<EntityEntry<Product>> CreateProductAsync(ProductViewModel productModel);
        public List<Product> GetAll();
        public Product GetId(int id);
        public bool EditProductAsync(int id, ProductViewModel productModel);
        public bool DeleteProductAsync(int id);
    }
}

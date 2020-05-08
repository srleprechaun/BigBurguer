using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "numeric(10,2)")]
        public decimal Price { get; set; }

        public ICollection<ProductIngredient> ProductIngredient { get; set; }
    }
}

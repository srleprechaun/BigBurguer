using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int StockQuantity { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}

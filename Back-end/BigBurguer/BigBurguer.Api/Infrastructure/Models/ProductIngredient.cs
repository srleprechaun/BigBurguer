
using System.Text.Json.Serialization;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class ProductIngredient
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public Ingredient Ingredient { get; set; }
    }
}

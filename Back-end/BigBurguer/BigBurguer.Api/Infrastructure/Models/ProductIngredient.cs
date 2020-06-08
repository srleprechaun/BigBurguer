using System.Text.Json.Serialization;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}

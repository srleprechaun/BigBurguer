namespace BigBurguer.Api.Infrastructure.Models
{
    public class ProductIngredient
    {
        public int? ProductId { get; set; }
        public int? IngredientId { get; set; }

        public Product Product { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}

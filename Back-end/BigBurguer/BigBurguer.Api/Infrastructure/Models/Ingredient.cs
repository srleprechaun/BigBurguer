using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int stockQuantity { get; set; }
    }
}

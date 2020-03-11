using System;

namespace BigBurguer.Api.Views
{
    public class Product
    {
        public string ImageUrl { get; set;  }
        public string Name { get; set; }      
        public string Ingredients { get; set; }  
        public decimal Price { get; set; } 
    }
}

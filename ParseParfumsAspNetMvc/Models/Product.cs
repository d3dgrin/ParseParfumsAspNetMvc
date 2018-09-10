using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseParfumsAspNetMvc.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PriceOrigin { get; set; }
        public decimal Price { get; set; }
        public string ImageSrc { get; set; }
        public ICollection<PriceProduct> Prices { get; set; }

        public Product()
        {
            Prices = new List<PriceProduct>();
        }
    }
}
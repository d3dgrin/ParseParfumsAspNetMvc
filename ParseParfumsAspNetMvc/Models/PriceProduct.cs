using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseParfumsAspNetMvc.Models
{
    public class PriceProduct
    {
        public int Id { get; set; }
        public string PriceOrigin { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
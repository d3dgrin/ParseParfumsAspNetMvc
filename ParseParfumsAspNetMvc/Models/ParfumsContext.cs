using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParseParfumsAspNetMvc.Models
{
    public class ParfumsContext : DbContext
    {
        public ParfumsContext()
            :base("ParfumsConnection")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<PriceProduct> Prices { get; set; }

    }
}
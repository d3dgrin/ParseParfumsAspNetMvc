using ParseParfumsAspNetMvc.Interfaces;
using ParseParfumsAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParseParfumsAspNetMvc.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ParfumsContext db;

        public ProductRepository()
        {
            this.db = new ParfumsContext();
        }

        public void Add(Product product)
        {
            db.Products.Add(product);
        }

        public void AddRange(List<Product> products)
        {
            db.Products.AddRange(products);
        }

        public Product Get(int? id)
        {
            return db.Products.Find(id);
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public IQueryable<PriceProduct> GetAllPrices(int? productId)
        {
            return db.Prices.Where(p => p.ProductId == productId).OrderByDescending(p => p.Date);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
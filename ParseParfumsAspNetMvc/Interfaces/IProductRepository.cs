using ParseParfumsAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseParfumsAspNetMvc.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        List<Product> GetAll();
        IQueryable<PriceProduct> GetAllPrices(int? productId);
        Product Get(int? id);
        void Add(Product product);
        void AddRange(List<Product> products);
        void Save();
    }
}

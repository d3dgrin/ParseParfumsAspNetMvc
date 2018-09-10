using Ninject.Modules;
using ParseParfumsAspNetMvc.Interfaces;
using ParseParfumsAspNetMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseParfumsAspNetMvc.Utils
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}
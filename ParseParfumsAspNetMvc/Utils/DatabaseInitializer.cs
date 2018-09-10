using ParseParfumsAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParseParfumsAspNetMvc.Utils
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ParfumsContext>
    {
        protected override void Seed(ParfumsContext context)
        {
            base.Seed(context);
        }
    }
}
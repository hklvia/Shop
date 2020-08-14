using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo1.Models;

namespace WebDemo1.DB
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductAttrKey> ProductAttrKey { get; set; }
        public DbSet<ProductAttrValue> ProductAttrValue { get; set; }
    }
}

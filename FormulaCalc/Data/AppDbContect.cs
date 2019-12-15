using Data_lib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaCalc.Data
{
    public class AppDbContext : DbContext
    {

        #region database sets for products and ingredients using entityframework 
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        #endregion


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // set unique name for entity of type product on model creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

        }
    }
}

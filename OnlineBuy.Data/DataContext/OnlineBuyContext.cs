using Microsoft.EntityFrameworkCore;
using OnlineBuy.Data.ConfigModels;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.DataContext
{
    public class OnlineBuyContext : DbContext
    {
        public static string ConnectionString;        
       
        public OnlineBuyContext(DbContextOptions option) : base(option)
        {            
        }

        public OnlineBuyContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerSmsCode> CustomerSmsCodes { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new CustomerSmsCodeConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductPriceConfig());
            modelBuilder.ApplyConfiguration(new ProductUnitConfig());

            base.OnModelCreating(modelBuilder);
        }        

    }
}

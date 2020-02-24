using Microsoft.EntityFrameworkCore;
using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace OnlineBuy.Repository.Repository.Implements
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext dbcontext) : base(dbcontext)
        {

        }

        public  bool ExistNewProductVersionAsync(string version)
        {
            //var product = GetAll().LastOrDefault();
            //return product.Version == version;
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await GetAllAsync();
        }
    }
}

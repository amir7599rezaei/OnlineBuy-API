using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        bool ExistNewProductVersionAsync(string version);

        Task<IEnumerable<Product>> GetProductsAsync();
    }
}

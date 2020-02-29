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

        public IEnumerable<object> GetProducts()
        {
            using (Data.DataContext.OnlineBuyContext context = new Data.DataContext.OnlineBuyContext())
            {
                return (from p in context.Products
                        join pp in context.ProductPrices on p.Id equals pp.ProductId
                        join pu in context.ProductUnits on pp.ProductUnitId equals pu.Id
                        select new
                        {
                            p.Id,
                            ProductName = p.Name,
                            p.Title,
                            p.Description,
                            pp.PrimaryPrice,
                            pp.FinalPrice,
                            pp.ShowPrice,
                            pp.OffPrice,
                            pp.OffPercent,
                            UnitName = pu.Name
                        }).ToArray();
            }
        }
    }
}

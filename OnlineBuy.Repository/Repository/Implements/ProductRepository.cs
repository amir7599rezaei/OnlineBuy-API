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

        public double GetFinalPrice(string productId)
        {
            using (Data.DataContext.OnlineBuyContext context = new Data.DataContext.OnlineBuyContext())
            {
                return (from p in context.Products
                        join pp in context.ProductPrices on p.Id equals pp.ProductId
                        where p.Id == productId
                        select pp.FinalPrice).FirstOrDefault();
            }
        }

        public IEnumerable<object> GetProducts()
        {
            using (Data.DataContext.OnlineBuyContext context = new Data.DataContext.OnlineBuyContext())
            {

                return (from p in context.Products
                        join img in context.ProductImages on p.Id equals img.ProductId into gl
                        from imgDef in gl.DefaultIfEmpty()
                        join pp in context.ProductPrices on p.Id equals pp.ProductId
                        join pu in context.ProductUnits on pp.ProductUnitId equals pu.Id
                        select new
                        {
                            ProductName = p.Name,
                            p.Title,
                            p.Description,
                            pp.PrimaryPrice,
                            pp.FinalPrice,
                            pp.ShowPrice,
                            pp.OffPrice,
                            pp.OffPercent,
                            UnitName = pu.Name,
                            ShowInCartCount = false,
                            CartCount = 1,
                            Image = imgDef != null ? imgDef.FormatContent +
                            Convert.ToBase64String(imgDef.Content, 0, imgDef.Content.Length) : string.Empty,
                            ProductUnitId = pu.Id

                        }).ToArray();
            }
        }
    }
}

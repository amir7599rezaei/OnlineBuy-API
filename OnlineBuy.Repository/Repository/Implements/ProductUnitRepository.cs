using Microsoft.EntityFrameworkCore;
using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Repository.Repository.Implements
{
    public class ProductUnitRepository : Repository<ProductUnit>, IProductUnitRepository
    {
        public ProductUnitRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}

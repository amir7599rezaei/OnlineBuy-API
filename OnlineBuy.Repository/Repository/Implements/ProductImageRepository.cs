using Microsoft.EntityFrameworkCore;
using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Repository.Repository.Implements
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public byte[] ConvertBase64ToByte(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public string ConvertByteToBase64(byte[] imageByte)
        {
            return Convert.ToBase64String(imageByte, 0, imageByte.Length);
        }
    }
}

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

            string convert = base64.Replace("data:image/png;base64,", String.Empty);
            return Convert.FromBase64String(convert);
        }

        public string ConvertByteToBase64(byte[] imageByte)
        {
            return Convert.ToBase64String(imageByte, 0, imageByte.Length);
        }
    }
}

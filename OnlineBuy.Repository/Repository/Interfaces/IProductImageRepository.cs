﻿using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Repository.Repository.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        string ConvertByteToBase64(byte[] imageByte);

        byte[] ConvertBase64ToByte(string base64);
    }
}

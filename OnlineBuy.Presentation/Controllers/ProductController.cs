﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBuy.Common.Enums;
using OnlineBuy.Common.Messages.Persian;
using OnlineBuy.Common.ResultApiStructure;
using OnlineBuy.Common.Utility;
using OnlineBuy.Data.DataContext;
using OnlineBuy.Data.Models;
using OnlineBuy.Presentation.CustomFilters;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using static OnlineBuy.Data.ViewModels.ProductViewModel;

namespace OnlineBuy.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : MyBaseController
    {
        public ProductController(IUnitOfWork<OnlineBuyContext> db) : base(db)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ProductRegister productRegister)
        {
            var product = new Product
            {
                Name = productRegister.Name,
                Title = productRegister.Title,
                IsExits = productRegister.IsExits,
                Provider = productRegister.Provider,
                BarcodeNumber = productRegister.BarcodeNumber,
                Description = productRegister.Description,
                ProductDate = productRegister.ProductDate,
                ExpireDate = productRegister.ExpireDate,
                CategoryId = productRegister.CategoryId
            };

            await _db.ProductRepository.InsertAsync(product);

            var productPrices = new List<ProductPrice>();
            foreach (var item in productRegister.ProductPriceRegisters)
            {
                productPrices.Add(new ProductPrice
                {
                    PrimaryPrice = item.PrimaryPrice,
                    FinalPrice = item.FinalPrice,
                    ShowPrice = item.FinalPrice,
                    OffPrice = item.OffPrice,
                    OffPercent = item.OffPercent,
                    ProductId = product.Id,
                    ProductUnitId = item.ProductUnitId
                });
            }

            await _db.ProductPriceRepository.InsertRangeAsync(productPrices);
            var res = await _db.SaveAsync();

            if (res > 0)
            {
                return Ok(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductTitle,
                    Code = (int)StatusMethods.SuccessRegister,
                    Status = StatusMethods.SuccessRegister.GetTitle(),
                    Message = StatusMethods.SuccessRegister.GetDescription()
                });
            }
            else
            {
                return BadRequest(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductTitle,
                    Code = (int)StatusMethods.OperationFailed,
                    Status = StatusMethods.OperationFailed.GetTitle(),
                    Message = StatusMethods.OperationFailed.GetDescription()
                });
            }
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            return Ok(new
            {
                Products = _db.ProductRepository.GetProducts()
            });
        }

        [HttpPost("cartRegister")]
        [Authorized]
        public async Task<IActionResult> CartRegister(CartRegister cartRegister)
        {
            var customerOrders = new List<CustomerOrder>();
            foreach (var item in cartRegister.ProductOrders)
            {
                customerOrders.Add(new CustomerOrder
                {
                    CustomerId = GetIdentifyCode(),
                    ProductId = item.ProductId,
                    CartCount = item.CartCount,
                    FinalPrice = _db.ProductRepository.GetFinalPrice(item.ProductId),
                    ProductUnitId = item.ProductUnitId,
                    OrderDate = DateTime.Now
                });
            }

            await _db.CustomerOrderRepository.InsertRangeAsync(customerOrders);
            var res = await _db.SaveAsync();
            if (res > 0)
            {
                return Ok(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductTitle,
                    Code = (int)StatusMethods.SuccessRegister,
                    Status = StatusMethods.SuccessRegister.GetTitle(),
                    Message = StatusMethods.SuccessRegister.GetDescription()
                });
            }
            else
            {
                return BadRequest(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductTitle,
                    Code = (int)StatusMethods.OperationFailed,
                    Status = StatusMethods.OperationFailed.GetTitle(),
                    Message = StatusMethods.OperationFailed.GetDescription()
                });
            }
        }

        [HttpPost("imageRegister")]
        public async Task<IActionResult> ImageRegister(ImgaeRegister imgaeRegister)
        {
            var productImage = new ProductImage
            {
                ProductId = imgaeRegister.ProductId,
                Content = _db.ProductImageRepository.ConvertBase64ToByte(imgaeRegister.Image),
                FormatContent = "data:image/png;base64,"
            };

            await _db.ProductImageRepository.InsertAsync(productImage);
            var res = await _db.SaveAsync();

            if (res > 0)
            {
                return Ok(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductTitle,
                    Code = (int)StatusMethods.SuccessRegister,
                    Status = StatusMethods.SuccessRegister.GetTitle(),
                    Message = StatusMethods.SuccessRegister.GetDescription()
                });
            }
            else
            {
                return Ok(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductTitle,
                    Code = (int)StatusMethods.OperationFailed,
                    Status = StatusMethods.OperationFailed.GetTitle(),
                    Message = StatusMethods.OperationFailed.GetDescription()
                });
            }
        }        
    }
}
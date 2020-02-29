using System;
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
using OnlineBuy.Repository.Infrastructure.Interfaces;
using static OnlineBuy.Data.ViewModels.ProductUnitViewModel;

namespace OnlineBuy.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductUnitController : MyBaseController
    {
        public ProductUnitController(IUnitOfWork<OnlineBuyContext> db) : base(db)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ProductUnitRegister productUnitRegister)
        {
            var productUnit = new ProductUnit
            {
                Name = productUnitRegister.Name
            };
            await _db.ProductUnitRepository.InsertAsync(productUnit);
            var res = await _db.SaveAsync();

            if (res > 0)
            {
                return Ok(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductUnitTitle,
                    Code = (int)StatusMethods.SuccessRegister,
                    Status = StatusMethods.SuccessRegister.GetTitle(),
                    Message = StatusMethods.SuccessRegister.GetDescription()
                });
            }
            else
            {
                return BadRequest(new ReturnApiMessages
                {
                    Title = PersianMessages.ProductUnitTitle,
                    Code = (int)StatusMethods.OperationFailed,
                    Status = StatusMethods.OperationFailed.GetTitle(),
                    Message = StatusMethods.OperationFailed.GetDescription()
                }); ;
            }
        }

        [HttpGet("productUnits")]
        public async Task<IActionResult> ProductUnits()
        {
            return Ok(new
            {
                ProductUnits = await _db.ProductUnitRepository.GetAllAsync()
            });

        }
    }
}
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
using OnlineBuy.Presentation.CustomFilters;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using static OnlineBuy.Data.ViewModels.CategoryViewModel;

namespace OnlineBuy.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : MyBaseController
    {
        public CategoryController(IUnitOfWork<OnlineBuyContext> db) : base(db)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CategoryRegister categoryRegister)
        {
            var category = new Category
            {
                Name = categoryRegister.Name
            };

            await _db.CategoryRepository.InsertAsync(category);
            await _db.SaveAsync();

            return Ok(new ReturnApiMessages
            {
                Title = PersianMessages.CategoryTitle,
                Code = (int)StatusMethods.SuccessRegister,
                Status = StatusMethods.SuccessRegister.GetTitle(),
                Message = StatusMethods.SuccessRegister.GetDescription(),
            });
        }           
    }
}
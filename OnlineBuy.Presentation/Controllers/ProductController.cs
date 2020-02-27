using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBuy.Data.DataContext;
using OnlineBuy.Repository.Infrastructure.Interfaces;

namespace OnlineBuy.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : MyBaseController
    {
        public ProductController(IUnitOfWork<OnlineBuyContext> db) : base(db)
        {
        }
    }
}
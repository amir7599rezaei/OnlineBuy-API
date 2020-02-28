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
    public class TestController : MyBaseController
    {
        public TestController(IUnitOfWork<OnlineBuyContext> db) : base(db)
        {
        }

        [HttpGet("testLog")]
        public IActionResult TestLog()
        {
            Log("test");
            return Ok(new
            {
                success = "success"
            });
        }
    }
}
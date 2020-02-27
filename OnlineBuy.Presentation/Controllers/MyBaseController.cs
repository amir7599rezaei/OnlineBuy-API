using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineBuy.Data.DataContext;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using OnlineBuy.Service.JWT.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuy.Presentation.Controllers
{
    public class MyBaseController : ControllerBase
    {

        protected readonly IUnitOfWork<OnlineBuyContext> _db;
        protected readonly IConfiguration _config;
        protected readonly IJsonWebTokensService _jwt;

        public MyBaseController(IUnitOfWork<OnlineBuyContext> db,
            IConfiguration config, IJsonWebTokensService jwt)
        {
            this._db = db;
            this._config = config;
            this._jwt = jwt;
        }

        public MyBaseController(IUnitOfWork<OnlineBuyContext> db)
        {
            this._db = db;
        }

        protected string GetIdentifyCode()
        {
            var identify = User.Claims.FirstOrDefault(x => x.Type.ToLower()
             .Contains("nameidentifier", StringComparison.InvariantCultureIgnoreCase));
            return identify != null ? identify.Value : string.Empty;
        }

        protected TimeSpan GetCurrentTime() =>
            TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));


    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuy.Presentation.Controllers
{
    public class MyBaseController : ControllerBase
    {
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

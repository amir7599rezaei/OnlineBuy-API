using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OnlineBuy.Service.JWT.Interface
{
    public interface IJsonWebTokensService
    {
        string CreateToken(string keyValue, Claim[] calims, DateTime duration);
    }
}

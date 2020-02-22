using OnlineBuy.Common.Enums;
using OnlineBuy.Data.Models;
using OnlineBuy.Data.ViewModels;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Repository.Interfaces
{
    public interface ICustomerSmsCodeRepository : IRepository<CustomerSmsCode>
    {
        int GenerateCode();
        RecieveSms ValidReciveCode(string customerId, int code);

        Task<bool> CustomerIsRecievedCode(string customerId);
    }
}

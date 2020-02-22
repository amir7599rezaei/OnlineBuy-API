using Microsoft.EntityFrameworkCore;
using OnlineBuy.Common.Enums;
using OnlineBuy.Data.Models;
using OnlineBuy.Data.ViewModels;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Repository.Implements
{
    public class CustomerSmsCodeRepository : Repository<CustomerSmsCode>, ICustomerSmsCodeRepository
    {
        private readonly Random rnd;
        public CustomerSmsCodeRepository(DbContext dbContext) : base(dbContext)
        {
            this.rnd = new Random();
        }

        public async Task<bool> CustomerIsRecievedCode(string customerId)
        {
            return await GetAsync(c => c.CustomerId == customerId && c.RecievedTime != TimeSpan.Zero) != null;
        }

        public int GenerateCode()
        {
            return rnd.Next(123000, 999999);
        }

        public RecieveSms ValidReciveCode(string customerId, int code)
        {
            var smsCustomer = Get(c => c.CustomerId == customerId && c.SmsCode == code);

            if (smsCustomer == null)
            {
                return RecieveSms.WrongCodeOrMobile;
            }

            var recieveTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            if (recieveTime.Subtract(smsCustomer.SentTime).TotalMinutes > smsCustomer.DurationCode)
            {
                return RecieveSms.PastTime;
            }

            return RecieveSms.SuccessCode;
        }
    }
}

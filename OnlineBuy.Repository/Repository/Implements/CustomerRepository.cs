using Microsoft.EntityFrameworkCore;
using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Repository.Implements
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbcontext) : base(dbcontext)
        {

        }

        public async Task<Customer> ExistCustomerAsync(string userName)
        {
            return await GetAsync(c => c.UserName == userName);
        }

        public async Task<bool> IsActiveCustomerAsync(string userName)
        {
            var c = await GetAsync(c => c.UserName == userName);
            return c.IsActive;
        }
    }
}

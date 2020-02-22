using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> ExistCustomerAsync(string userName);

        Task<bool> IsActiveCustomerAsync(string userName);
        
    }
}

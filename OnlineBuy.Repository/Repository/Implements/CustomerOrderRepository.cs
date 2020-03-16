using Microsoft.EntityFrameworkCore;
using OnlineBuy.Data.Models;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineBuy.Data.DataContext;
using System.Linq;

namespace OnlineBuy.Repository.Repository.Implements
{
    public class CustomerOrderRepository : Repository<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(DbContext db) : base(db)
        {

        }

        public IEnumerable<object> GetOrders(string customerId)
        {
            using (OnlineBuyContext context=new OnlineBuyContext())
            {
                return (from p in context.Products
                        join c in context.CustomerOrders on p.Id equals c.ProductId
                        where c.CustomerId == customerId
                        select new
                        {
                            p.Name,
                            p.Title,
                            c.CartCount,
                            c.FinalPrice,
                            c.OrderDate,
                            c.StatusText

                        }).ToArray();
            }
        }
    }
}

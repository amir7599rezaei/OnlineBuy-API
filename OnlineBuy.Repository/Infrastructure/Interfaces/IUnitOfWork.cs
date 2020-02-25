using Microsoft.EntityFrameworkCore;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Infrastructure.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        public ICustomerRepository CustomerRepository { get; }
        public IProductRepository ProductRepository { get; }        
        public ICustomerSmsCodeRepository CustomerSmsCodeRepository { get; }
        public IProductPriceRepository ProductPriceRepository { get;  }
        public IProductUnitRepository ProductUnitRepository { get;  }
        public IProductImageRepository ProductImageRepository { get;  }

        void Save();

        Task<int> SaveAsync();
    }

}

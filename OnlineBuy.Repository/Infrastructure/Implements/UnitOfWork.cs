﻿using Microsoft.EntityFrameworkCore;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using OnlineBuy.Repository.Repository.Implements;
using OnlineBuy.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Infrastructure.Implements
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext, new()
    {
        protected readonly DbContext _db;

        public UnitOfWork()
        {
            _db = new TContext();
        }

        #region//Prperty of models
        private ICustomerRepository customerRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(_db);
                }
                return customerRepository;
            }
        }


        private IProductRepository productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_db);
                }
                return productRepository;
            }
        }


        private ICustomerSmsCodeRepository customerSmsCodeRepository;
        public ICustomerSmsCodeRepository CustomerSmsCodeRepository
        {
            get
            {
                if (customerSmsCodeRepository == null)
                {
                    customerSmsCodeRepository = new CustomerSmsCodeRepository(_db);
                }
                return customerSmsCodeRepository;
            }

        }

        private IProductPriceRepository productPriceRepository;
        public IProductPriceRepository ProductPriceRepository
        {
            get
            {
                if (productPriceRepository == null)
                {
                    productPriceRepository = new ProductPriceRepository(_db);
                }
                return productPriceRepository;
            }
        }

        private IProductUnitRepository productUnitRepository;
        public IProductUnitRepository ProductUnitRepository
        {
            get
            {
                if (productUnitRepository == null)
                {
                    productUnitRepository = new ProductUnitRepository(_db);
                }
                return productUnitRepository;
            }

        }

        private IProductImageRepository productImageRepository;

        public IProductImageRepository ProductImageRepository
        {
            get
            {
                if (productImageRepository == null)
                {
                    productImageRepository = new ProductImageRepository(_db);
                }
                return productImageRepository;
            }
        }


        private ICategoryRepository categoryRepository;

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(_db);
                }
                return categoryRepository;
            }
        }

        private ICustomerOrderRepository customerOrderRepository;

        public ICustomerOrderRepository CustomerOrderRepository
        {
            get
            {
                if (customerOrderRepository == null)
                {
                    customerOrderRepository = new CustomerOrderRepository(_db);
                }
                return customerOrderRepository;
            }
        }


        #endregion


        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _db.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}

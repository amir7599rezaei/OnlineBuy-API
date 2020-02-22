using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Infrastructure.Implements
{
    public abstract class Repository<TEntity>
        where TEntity:class,new()
    {
        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbset;

        public Repository(DbContext db)
        {
            this._db = db;
            this._dbset = _db.Set<TEntity>();
        }

        #region //normal methods       
        public void Delete(object id)
        {
            var entity = GetById(id);
            _ = entity ?? throw new ArgumentException("there is no entity");
            _dbset.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            var entities = _dbset.Where(where).AsEnumerable();
            _dbset.RemoveRange(entities);
        }


        public TEntity GetById(object id)
        {
            return _dbset.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbset.AsEnumerable();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).AsEnumerable();
        }


        public void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }


        public void Update(TEntity entity)
        {
            _ = entity ?? throw new ArgumentException("The entity is snull");
            _dbset.Update(entity);
        }
        #endregion

        #region Async Methods      
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbset.Where(where).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbset.Where(where).ToListAsync();
        }


        public async Task InsertAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }

        #endregion

        #region disposing
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposed)
                    _db.Dispose();
            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        ~Repository()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

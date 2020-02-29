using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBuy.Repository.Infrastructure.Interfaces
{
    public interface IRepository<TEntity>:IDisposable
        where TEntity:class
    {
        #region //----------------normal methods-----------------
        void Insert(TEntity entity);
        void InsertRange(IList<TEntity> entities);
        void Update(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
        

        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        #endregion


        #region//------------- async methods-----------------------
        Task InsertAsync(TEntity entity);
        Task InsertRangeAsync(IList<TEntity> entities);
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        #endregion
    }
}

using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{

    public interface IRepositoryBase<T> where T : ModelBase
    {

        #region Sync

        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQuery();
        int Count(Expression<Func<T, bool>> predicate);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> WithInclude(params Expression<Func<T, object>>[] includeExpressions);
        T Get(int Id);
        void Insert(T entry);
        void Delete(int entryId);
        void Delete(T entry);
        void Update(T entry);
        void InsertOrUpdate(T entry);

        #endregion

        #region Async

        Task<IEnumerable<T>> GetAllAsync();
        //Task<IQueryable<T>> GetAllQueryAsync(); // has no async method
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        //Task<IQueryable<T>> WithIncludeAsync(params Expression<Func<T, object>>[] includeExpressions); // has no async method
        Task<T> GetAsync(int Id);
        Task InsertAsync(T entry);
        Task DeleteAsync(int entryId);
        Task DeleteAsync(T entry);
        //Task UpdateAsync(T entry); // has no async method
        Task InsertOrUpdateAsync(T entry);
        Task TruncateAsync();


        #endregion
    }

}

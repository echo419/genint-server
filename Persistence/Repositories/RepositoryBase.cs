using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Core.Models;
using Core.Repositories;
using Core.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories
{

    public class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase
    {

        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        //static readonly object thisLock = new object();



        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }



        #region sync

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T> GetAllQuery()
        {
            return _dbSet;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> WithInclude(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }

            //return set.ToList();
            return set;
        }

        public virtual T Get(int Id)
        {
            return _dbSet.Where(r => r.Id == Id).FirstOrDefault();
        }

        public virtual void Insert(T entry)
        {
            _dbSet.Add(entry);
        }

        public virtual void Delete(int entryId)
        {
            T entity = _dbSet.Where(r => r.Id == entryId).FirstOrDefault();
            _dbSet.Remove(entity);
        }

        public virtual void Delete(T entry)
        {
            Delete(entry.Id);
        }

        public virtual void Update(T entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
            _dbSet.Attach(entry);
        }

        public virtual void InsertOrUpdate(T entry)
        {
            var localEntry = _dbSet.Where(r => r.Id == entry.Id);

            if (localEntry == null)
            {
                Insert(entry);
            }
            else
            {
                _context.Entry(localEntry).State = EntityState.Detached;
                Update(entry);
            }
        }

        #endregion

        #region async

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetAsync(int Id)
        {
            return await _dbSet.Where(r => r.Id == Id).FirstOrDefaultAsync();
        }

        public virtual async Task DeleteAsync(int entryId)
        {
            T entity = await _dbSet.Where(r => r.Id == entryId).FirstOrDefaultAsync();
            _dbSet.Remove(entity);
        }

        public virtual async Task DeleteAsync(T entry)
        {
            await DeleteAsync(entry.Id);
        }

        public virtual async Task TruncateAsync()
        {
            await _dbSet.ExecuteDeleteAsync();
        }

        public async Task InsertAsync(T entry)
        {
            await _dbSet.AddAsync(entry);
        }

        public async Task InsertOrUpdateAsync(T entry)
        {
            var localEntry = _dbSet.Where(r => r.Id == entry.Id);

            if (localEntry == null)
            {
                await InsertAsync(entry);
            }
            else
            {
                _context.Entry(localEntry).State = EntityState.Detached;
                Update(entry);
            }
        }

        #endregion

    }

}
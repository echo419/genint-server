using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Core;
using Core.Models;
using Core.Repositories;
using Persistence.Repositories;


namespace Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ITestRepository Tests { get; }
        public IRepositoryBase<User> Users { get; }

        //static readonly object thisLock = new object();


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new RepositoryBase<User>(context);
            Tests = new TestRepository();
        }


        public IRepositoryBase<T> GetRepo<T>() where T : ModelBase
        {
            RepositoryBase<T> repo = new RepositoryBase<T>(_context);
            return repo;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region IDisposable

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DetachAll()
        {

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry dbEntityEntry 
                in _context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList())
            {
                dbEntityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }



        }

        #endregion
    }

}
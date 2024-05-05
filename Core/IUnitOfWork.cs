using Core.Models;
using Core.Repositories;

namespace Core
{

    public interface IUnitOfWork
    {
        ITestRepository Tests { get; }

        IRepositoryBase<T> GetRepo<T>() where T : ModelBase;

        void Complete();

        void Dispose();
        void DetachAll();

        Task CommitAsync();
    }

}
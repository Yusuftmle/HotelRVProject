
namespace HotelRv.Infrastructure.Persistence.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void Dispose();
        Task RollbackTransactionAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
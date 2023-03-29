using BaseProject.Data.Interface;

namespace BaseProject.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }

        Task<int> Commit();
    }
}

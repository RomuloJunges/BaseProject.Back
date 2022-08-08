using BaseProject.Data.Context;
using BaseProject.Data.Interface;

namespace BaseProject.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseProjectContext _context;
        
        public IUserRepository User { get; }

        public UnitOfWork(BaseProjectContext context, IUserRepository user)
        {
            _context = context;
            User = user;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose) _context.Dispose();
        }
    }
}

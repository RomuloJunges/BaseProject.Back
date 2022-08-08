using BaseProject.Data.Context;
using BaseProject.Data.Interface;

namespace BaseProject.Data.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly BaseProjectContext _context;

        public GenericRepository(BaseProjectContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public List<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList<T>();
        }
    }
}

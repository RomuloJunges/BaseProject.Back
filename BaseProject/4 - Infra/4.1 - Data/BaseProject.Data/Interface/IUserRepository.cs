using BaseProject.Domain.Identity;

namespace BaseProject.Data.Interface
{
    public interface IUserRepository : IGenericRepository
    {
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
    }
}
using BaseProject.Data.Context;
using BaseProject.Data.Interface;
using BaseProject.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data.Repository
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly BaseProjectContext _context;

        public UserRepository(BaseProjectContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetById(Guid id) => await _context.Users.FindAsync(id);

        public async Task<User> GetByEmail(string email) =>
            await _context.Users
                .SingleOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
    }
}

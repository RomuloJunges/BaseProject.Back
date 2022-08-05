using BaseProject.Domain.Entities;
using BaseProject.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            CreationDate = DateTime.Now;
            ChangePassword = false;
            Status = Status.Active;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ChangePassword { get; set; }
        public DateTime CreationDate { get; set; }
        public Status Status { get; set; }
        public IList<Address> Addresses { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}

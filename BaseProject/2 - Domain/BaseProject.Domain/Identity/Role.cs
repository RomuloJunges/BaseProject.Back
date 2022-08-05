using Microsoft.AspNetCore.Identity;

namespace BaseProject.Domain.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public IList<UserRole> UserRoles { get; set; }
    }
}

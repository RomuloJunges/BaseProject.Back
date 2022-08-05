using BaseProject.Domain.Entities;
using BaseProject.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data.Context
{
    public class BaseProjectContext : IdentityDbContext<User, Role, Guid,
                                                        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
                                                        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public BaseProjectContext(DbContextOptions<BaseProjectContext> options) : base(options) { }

        public DbSet<User?> Users { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}

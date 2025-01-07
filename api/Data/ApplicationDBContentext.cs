using api.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<InteractionType> InteractionTypes { get; set; }
        public DbSet<PostInteraction> PostInteractions { get; set; }
        public DbSet<CommentInteraction> CommentInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserPermission>().HasKey(up => new { up.UserId, up.PermissionId });
            builder.Entity<RolePermissions>().HasKey(rp => new { rp.RoleId, rp.PermissionId });
        }

    }
}
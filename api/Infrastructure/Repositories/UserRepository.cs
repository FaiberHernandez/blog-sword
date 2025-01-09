using api.Data;
using api.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<string>> GetPermissionsByUserIdAsync(string userId)
        {
            var permissions = await _dbContext.UserPermissions.Where(up => up.UserId == userId).Select(up => up.Permission.Key).ToListAsync();
            return permissions.AsReadOnly();
        }

        public async Task<IReadOnlyCollection<string>> GetRolesByUserIdAsync(string userId)
        {
            var roles = await _dbContext.UserPermissions
                .Where(x => x.UserId == userId)
                .SelectMany(user => user.Permission.RolePermissions.Select(rol => rol.Role.Key))
                .Distinct()
                .ToListAsync();
            return roles.AsReadOnly();
        }
    }
}
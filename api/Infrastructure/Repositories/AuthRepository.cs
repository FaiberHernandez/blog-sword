using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            var createdUser = await _userManager.CreateAsync(user, password);
            if (!createdUser.Succeeded) return createdUser;

            var roles = new[] { "Lector", "Editor" };
            foreach (var role in roles)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded) return roleResult;
            }

            return createdUser;
        }
    }
}
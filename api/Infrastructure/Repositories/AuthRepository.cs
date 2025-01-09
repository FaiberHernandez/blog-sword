using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded) return null;
            return user;
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
using api.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Infrastructure.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUserAsync(User user, string password);
    }
}
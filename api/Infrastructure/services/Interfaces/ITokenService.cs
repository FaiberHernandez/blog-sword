using api.Infrastructure.Models;

namespace api.Infrastructure.services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
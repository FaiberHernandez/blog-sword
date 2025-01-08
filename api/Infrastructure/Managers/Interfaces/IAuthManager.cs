using api.Infrastructure.Dtos.Auth;

namespace api.Infrastructure.Managers.Interfaces
{
    public interface IAuthManager
    {
        Task<NewUserDto> RegisterUserAsync(RegisterUserDto newUserDto);
    }
}
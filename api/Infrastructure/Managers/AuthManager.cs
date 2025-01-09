using api.Infrastructure.Dtos.Auth;
using api.Infrastructure.Managers.Interfaces;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using api.Infrastructure.services.Interfaces;

namespace api.Infrastructure.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        
        public AuthManager(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<NewUserDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _authRepository.LoginUserAsync(loginUserDto.Email, loginUserDto.Password);
            
            if(user == null) {
                throw new Exception("Invalid email and/or password.");
            }

            return new NewUserDto
            {
                Email = loginUserDto.Email,
                UserId = user.Id,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        public async Task<NewUserDto> RegisterUserAsync(RegisterUserDto newUserDto)
        {
            var newUser = new User
            {
                Email = newUserDto.Email,
                FirstName = newUserDto.FirstName,
                MiddleName = newUserDto.MiddleName,
                FirstSurname = newUserDto.FirstSurname,
                SecondSurname = newUserDto.SecondSurname,
                UserName = newUserDto.Email
            };

            var createdUser = await _authRepository.RegisterUserAsync(newUser, newUserDto.Password);

            if (!createdUser.Succeeded)
            {
                var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }

            var token = await _tokenService.GenerateToken(newUser);

            return new NewUserDto
            {
                Email = newUser.Email,
                UserId = newUser.Id,
                Token = token
            };
        }
    }
}
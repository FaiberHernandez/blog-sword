using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using api.Infrastructure.services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace api.Infrastructure.services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IUserRepository _userRepository;
        public TokenService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            _userRepository = userRepository;
        }

        public async Task<string> GenerateToken(User user)
        {
            var permissions =  await _userRepository.GetPermissionsByUserIdAsync(user.Id);
            var roles = await _userRepository.GetRolesByUserIdAsync(user.Id);
            var claims = new List<Claim>
            {
                new Claim(TokenClaims.UserId, user.Id),
                new Claim(TokenClaims.FirstName, user.FirstName),
                new Claim(TokenClaims.MiddleName, user.MiddleName ?? string.Empty),
                new Claim(TokenClaims.FirstSurname, user.FirstSurname),
                new Claim(TokenClaims.SecondSurname, user.SecondSurname ?? string.Empty),
                new Claim(TokenClaims.Email, user.Email!)
            };
            AddClaim(claims, permissions, TokenClaims.Permissions);
            AddClaim(claims, roles, TokenClaims.Roles);

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static void AddClaim(List<Claim> claims, IEnumerable<string> list, string claimName)
        {
            if (list.Any())
            {
                claims.AddRange(list.Select(policy => new Claim(claimName, policy)));
            }
        }

    }
}
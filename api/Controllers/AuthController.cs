using api.Infrastructure.Dtos.Auth;
using api.Infrastructure.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto newUserDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var newUser = await _authManager.RegisterUserAsync(newUserDto);
            return Ok(newUser);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto loginUserDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _authManager.LoginUserAsync(loginUserDto);
            return Ok(user);
        }  
        
    }
}
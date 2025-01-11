using api.Infrastructure;
using api.Infrastructure.Dtos.Post;
using api.Infrastructure.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostManager _postManager;

        public PostController(IPostManager postManager)
        {
            _postManager = postManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDto post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.GetClaimValue(TokenClaims.UserId);
            var newPostId = await _postManager.CreatePostAsync(post, userId);
            return Ok(newPostId);
        }
    }
}
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
        private readonly IInteractionManager _interactionManager;

        public PostController(IPostManager postManager, IInteractionManager interactionManager)
        {
            _postManager = postManager;
            _interactionManager = interactionManager;
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

        [HttpPost("{postId:int}/like")]
        [Authorize]
        public async Task<IActionResult> LikePostAsync([FromRoute] int postId)
        {
            var userId = User.GetClaimValue(TokenClaims.UserId);
            var newPostInteractionId = await _interactionManager.LikePostAsync(postId, userId);
            return Ok(newPostInteractionId);
        }

        [HttpDelete("interaction/{postInteractionId:int}")]
        [Authorize]
        public async Task<IActionResult> RemovePostInteractionAsync([FromRoute] int postInteractionId)
        {
            var userId = User.GetClaimValue(TokenClaims.UserId);
            await _interactionManager.RemovePostInteraction(postInteractionId, userId);
            return NoContent();
        }

        [HttpPost("{postId:int}/rate")]
        [Authorize]
        public async Task<IActionResult> RatePostAsync([FromRoute] int postId, [FromBody] RatePostRequestDto ratePost)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.GetClaimValue(TokenClaims.UserId);
            var newPostInteractionId = await _interactionManager.RatePostAsync(postId, userId, ratePost.Rate);
            return Ok(newPostInteractionId);
        }
        
    }
}
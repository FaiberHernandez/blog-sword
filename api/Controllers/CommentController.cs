using api.Infrastructure;
using api.Infrastructure.Dtos.Comment;
using api.Infrastructure.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentManager _commentManager;
        private readonly IInteractionManager _interactionManager;
        public CommentController(ICommentManager commentManager, IInteractionManager interactionManager)
        {
            _commentManager = commentManager;
            _interactionManager = interactionManager;
        }

        [HttpPost("{postId:int}")]
        [Authorize]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentDto comment, int postId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.GetClaimValue(TokenClaims.UserId);
            var newCommentId = await _commentManager.CreateCommentAsync(comment, userId, postId);
            return Ok(newCommentId);
        }

        [HttpPost("{commentId:int}/like")]
        [Authorize]
        public async Task<IActionResult> LikeCommentAsync(int commentId)
        {
            var userId = User.GetClaimValue(TokenClaims.UserId);
            var newLikeCommentInteractionId = await _interactionManager.LikeCommentAsync(commentId, userId);
            return Ok(newLikeCommentInteractionId);
        }

        [HttpDelete("interaction/{commentInteractionId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveCommentInteractionAsync(int commentInteractionId)
        {
            var userId = User.GetClaimValue(TokenClaims.UserId);
            await _interactionManager.RemoveCommentInteraction(commentInteractionId, userId);
            return NoContent();
        }

        [HttpPut("{commentId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCommentAsync([FromBody] CreateCommentDto comment, int commentId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.GetClaimValue(TokenClaims.UserId);
            await _commentManager.UpdateCommentAsync(comment, commentId, userId);
            return NoContent();
        }
    }
}
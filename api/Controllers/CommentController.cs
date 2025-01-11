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
        public CommentController(ICommentManager commentManager)
        {
            _commentManager = commentManager;
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
    }
}
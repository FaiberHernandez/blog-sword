using api.Infrastructure.Dtos.Comment;
using api.Infrastructure.Managers.Interfaces;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

namespace api.Infrastructure.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly ICommentRepository _commentRepository;

        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<int> CreateCommentAsync(CreateCommentDto comment, string userId, int postId)
        {
            var newComment = new Comment
            {
                Content = comment.Content,
                UserId = userId,
                PostId = postId,
                ParentCommentId = comment.ParentCommentId
            };

            _commentRepository.CreateComment(newComment);
            await _commentRepository.SaveChangesAsync();

            return newComment.Id;
        }
    }
}
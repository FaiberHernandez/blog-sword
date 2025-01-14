using api.Infrastructure.Dtos.Comment;
using api.Infrastructure.Managers.Interfaces;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

namespace api.Infrastructure.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IInteractionRepository _interactionRepository;

        public CommentManager(ICommentRepository commentRepository, IInteractionRepository interactionRepository)
        {
            _commentRepository = commentRepository;
            _interactionRepository = interactionRepository;
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

        public async Task RemoveCommentAsync(int commentId, string userId)
        {
            var commentToRemove = await _commentRepository.GetCommentByIdAsync(commentId);
            if(commentToRemove == null) throw new Exception("Comment not found");

            if(commentToRemove.UserId != userId) throw new Exception("User is not the owner of the comment");

            if(commentToRemove.CommentInteractions.Any()) {
                _interactionRepository.RemoveCommentInteractions(commentToRemove.CommentInteractions);
            }

            if(commentToRemove.Replies.Any()) {
                var repliesInteractions = commentToRemove.Replies.SelectMany(r => r.CommentInteractions).ToList();
                _interactionRepository.RemoveCommentInteractions(repliesInteractions);
                _commentRepository.RemoveComments(commentToRemove.Replies);
            }

            await _commentRepository.RemoveCommentAsync(commentToRemove);
        }

        public async Task UpdateCommentAsync(CreateCommentDto comment, int commentId, string userId)
        {
            var commentToUpdate = await _commentRepository.GetCommentByIdAsync(commentId);
            if(commentToUpdate == null) throw new Exception("Comment not found");

            if(commentToUpdate.UserId != userId) throw new Exception("User is not the owner of the comment");

            commentToUpdate.Content = comment.Content;

            await _commentRepository.SaveChangesAsync();
        }
    }
}
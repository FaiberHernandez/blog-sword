using api.Infrastructure.Models;

namespace api.Infrastructure.Repositories.Interfaces
{
    public interface IInteractionRepository
    {
        void AddPostInteraction(PostInteraction postInteraction);
        void AddCommentInteraction(CommentInteraction commentInteraction);
        Task RemovePostInteraction(PostInteraction postInteraction); 
        Task RemoveCommentInteraction(CommentInteraction commentInteraction);
        Task<InteractionType?> GetInteractionTypeByCodeAsync(string code);
        Task<bool> CheckIfUserHasPostInteractionAsync(int postId, string userId, int interactionTypeId);
        Task<bool> CheckIfUserHasCommentInteractionAsync(int commentId, string userId, int interactionTypeId);
        Task<PostInteraction?> GetPostInteractionByIdAsync(int postInteractionId);
        Task<CommentInteraction?> GetCommentInteractionByIdAsync(int commentInteractionId);
        Task SaveChangesAsync();
    }
}
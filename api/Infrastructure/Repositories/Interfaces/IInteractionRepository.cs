using api.Infrastructure.Models;

namespace api.Infrastructure.Repositories.Interfaces
{
    public interface IInteractionRepository
    {
        void AddPostInteraction(PostInteraction postInteraction);
        Task<InteractionType?> GetInteractionTypeByCodeAsync(string code);
        Task<bool> CheckIfUserHasPostInteractionAsync(int postId, string userId, int interactionTypeId);
        Task SaveChangesAsync();
    }
}
namespace api.Infrastructure.Managers.Interfaces
{
    public interface IInteractionManager
    {
        Task<int> LikePostAsync(int postId, string userId);
        Task RemovePostInteraction(int postInteractionId, string userId);
    }
}
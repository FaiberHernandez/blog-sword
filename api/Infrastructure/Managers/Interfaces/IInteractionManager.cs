namespace api.Infrastructure.Managers.Interfaces
{
    public interface IInteractionManager
    {
        Task<int> LikePostAsync(int postId, string userId);
        Task<int> RatePostAsync(int postId, string userId, int rate);
        Task RemovePostInteraction(int postInteractionId, string userId);
    }
}
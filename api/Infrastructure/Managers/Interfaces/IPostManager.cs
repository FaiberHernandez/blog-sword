using api.Infrastructure.Dtos.Post;

namespace api.Infrastructure.Managers.Interfaces
{
    public interface IPostManager
    {
        Task<int> CreatePostAsync(CreatePostDto post, string userId);
        Task UpdatePostAsync(CreatePostDto postUpdate, int postId, string userId);
        Task RemovePostAsync(int postId, string userId);
    }
}
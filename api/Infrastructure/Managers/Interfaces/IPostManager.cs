using api.Infrastructure.Dtos.Post;

namespace api.Infrastructure.Managers.Interfaces
{
    public interface IPostManager
    {
        Task<int> CreatePostAsync(CreatePostDto post, string userId);
    }
}
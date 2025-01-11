using api.Infrastructure.Dtos.Post;

namespace api.Infrastructure.Managers.Interfaces
{
    public interface IPostManager
    {
        Task<int> CreatePost(CreatePostDto post, string userId);
    }
}
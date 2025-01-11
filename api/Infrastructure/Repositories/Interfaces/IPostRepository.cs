using api.Infrastructure.Models;

namespace api.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository
    {
        void CreatePost(Post post);
        Task SaveChangesAsync();
    }
}
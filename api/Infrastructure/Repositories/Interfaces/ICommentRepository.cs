using api.Infrastructure.Models;

namespace api.Infrastructure.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void CreateComment(Comment comment);
        Task SaveChangesAsync();
    }
}
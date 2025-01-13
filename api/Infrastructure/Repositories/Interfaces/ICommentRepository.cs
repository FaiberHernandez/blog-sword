using api.Infrastructure.Models;

namespace api.Infrastructure.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void CreateComment(Comment comment);
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task SaveChangesAsync();
    }
}
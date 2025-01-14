using api.Infrastructure.Dtos.Comment;

namespace api.Infrastructure.Managers.Interfaces
{
    public interface ICommentManager
    {
        Task<int> CreateCommentAsync(CreateCommentDto comment, string userId, int postId);
        Task RemoveCommentAsync(int commentId, string userId);
        Task UpdateCommentAsync(CreateCommentDto comment, int commentId, string userId);
    }
}
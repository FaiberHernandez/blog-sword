using api.Data;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FindAsync(commentId);
        }

        public async Task<Comment?> GetCommentToDeleteByIdAsync(int commentId)
        {
            return await _context.Comments.Include(c => c.Replies).ThenInclude(r => r.CommentInteractions).Include(c => c.CommentInteractions).FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task RemoveCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public void RemoveComments(ICollection<Comment> comments)
        {
            _context.Comments.RemoveRange(comments);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
using api.Data;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

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

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
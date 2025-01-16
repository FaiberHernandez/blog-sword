using api.Data;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            return  await _context.Posts.FindAsync(postId);
        }

        public async Task<Post?> GetPostToDeleteByIdAsync(int postId)
        {
            return await _context.Posts.Include(p => p.Comments)
                .ThenInclude(c => c.Replies)
                .ThenInclude(r => r.CommentInteractions)
                .Include(p => p.Comments)
                .ThenInclude(c => c.CommentInteractions)
                .Include(p => p.PostInteractions)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task RemovePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
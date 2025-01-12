using api.Data;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
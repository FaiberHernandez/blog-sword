using api.Data;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Repositories
{
    public class InteractionRepository : IInteractionRepository
    {
        private readonly ApplicationDbContext _context;

        public InteractionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddPostInteraction(PostInteraction postInteraction)
        {
            _context.PostInteractions.Add(postInteraction);
        }

        public async Task<bool> CheckIfUserHasPostInteractionAsync(int postId, string userId, int interactionTypeId)
        {
            return await _context.PostInteractions.AnyAsync(x => x.PostId == postId && x.UserId == userId && x.InteractionTypeId == interactionTypeId);
        }

        public async Task<InteractionType?> GetInteractionTypeByCodeAsync(string code)
        {
            return await _context.InteractionTypes.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<PostInteraction?> GetPostInteractionByIdAsync(int postInteractionId)
        {
            return await _context.PostInteractions.FirstOrDefaultAsync(x => x.Id == postInteractionId);
        }

        public async Task RemovePostInteraction(PostInteraction postInteraction)
        {
            _context.PostInteractions.Remove(postInteraction);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
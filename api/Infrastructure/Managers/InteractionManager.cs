using api.Infrastructure.Managers.Interfaces;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

namespace api.Infrastructure.Managers
{
    public class InteractionManager : IInteractionManager
    {
        private readonly IInteractionRepository _interactionRepository;
        private readonly IPostRepository _postRepository;
        public InteractionManager(IInteractionRepository interactionRepository, IPostRepository postRepository)
        {
            _interactionRepository = interactionRepository;
            _postRepository = postRepository;
        }

        public async Task<int> LikePostAsync(int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) throw new Exception("Post not exists");

            var likeInteraction = await _interactionRepository.GetInteractionTypeByCodeAsync(InteractionTypes.Like);
            if (likeInteraction == null) throw new Exception("Interaction type Like not exists");

            var userHasLike = await _interactionRepository.CheckIfUserHasPostInteractionAsync(postId, userId, likeInteraction.Id);
            if (userHasLike) throw new Exception("User already liked this post");

            var newLikePostInteraction = new PostInteraction
            {
                PostId = postId,
                UserId = userId,
                Value = "1",
                InteractionTypeId = likeInteraction.Id
            };

            _interactionRepository.AddPostInteraction(newLikePostInteraction);
            await _interactionRepository.SaveChangesAsync();

            return newLikePostInteraction.Id;
        }

        public async Task<int> RatePostAsync(int postId, string userId, int rate)
        {
            if (rate < 1 || rate > 5) throw new Exception("Rate must be between 1 and 5");
            
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) throw new Exception("Post not exists");

            var rateInteraction = await _interactionRepository.GetInteractionTypeByCodeAsync(InteractionTypes.Rate);
            if (rateInteraction == null) throw new Exception("Interaction type Rate not exists");

            var userHasRate = await _interactionRepository.CheckIfUserHasPostInteractionAsync(postId, userId, rateInteraction.Id);
            if (userHasRate) throw new Exception("User already rated this post");

            var newRatePostInteraction = new PostInteraction
            {
                PostId = postId,
                UserId = userId,
                Value = rate.ToString(),
                InteractionTypeId = rateInteraction.Id
            };

            _interactionRepository.AddPostInteraction(newRatePostInteraction);
            await _interactionRepository.SaveChangesAsync();

            return newRatePostInteraction.Id;
        }

        public async Task RemovePostInteraction(int postInteractionId, string userId)
        {
            var postInteraction = await _interactionRepository.GetPostInteractionByIdAsync(postInteractionId);
            if (postInteraction == null) throw new Exception("Post interaction not exists");

            if (postInteraction.UserId != userId) throw new Exception("User not allowed to remove this interaction");
            
            await _interactionRepository.RemovePostInteraction(postInteraction);
        }        
    }
}
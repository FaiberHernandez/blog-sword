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

        public async Task RemovePostInteraction(int postInteractionId, string userId)
        {
            var postInteraction = await _interactionRepository.GetPostInteractionByIdAsync(postInteractionId);
            if (postInteraction == null) throw new Exception("Post interaction not exists");

            if (postInteraction.UserId != userId) throw new Exception("User not allowed to remove this interaction");
            
            await _interactionRepository.RemovePostInteraction(postInteraction);
        }
    }
}
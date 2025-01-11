using api.Infrastructure.Dtos.Post;
using api.Infrastructure.Managers.Interfaces;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

namespace api.Infrastructure.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postRepository;

        public PostManager(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<int> CreatePostAsync(CreatePostDto post, string userId)
        {
            var newPost = new Post
            {
                Title = post.Title,
                Content = post.Content,
                UserId = userId
            };

            _postRepository.CreatePost(newPost);
            await _postRepository.SaveChangesAsync();

            return newPost.Id;
        }
    }
}
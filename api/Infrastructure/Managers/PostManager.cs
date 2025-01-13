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

        public async Task UpdatePostAsync(CreatePostDto postUpdate, int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if(post == null) throw new Exception("Post not found");

            if(post.UserId != userId) throw new Exception("User is not the owner of the post");

            post.Title = postUpdate.Title;
            post.Content = postUpdate.Content;

            await _postRepository.SaveChangesAsync();
        }
    }
}
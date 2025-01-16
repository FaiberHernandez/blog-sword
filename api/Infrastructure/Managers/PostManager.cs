using api.Infrastructure.Dtos.Post;
using api.Infrastructure.Managers.Interfaces;
using api.Infrastructure.Models;
using api.Infrastructure.Repositories.Interfaces;

namespace api.Infrastructure.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly ICommentRepository _commentRepository;

        public PostManager(IPostRepository postRepository, IInteractionRepository interactionRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _interactionRepository = interactionRepository;
            _commentRepository = commentRepository;
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

        public async Task RemovePostAsync(int postId, string userId)
        {
            var postToRemove = await _postRepository.GetPostToDeleteByIdAsync(postId);
            if(postToRemove == null) throw new Exception("Post not found");

            if(postToRemove.UserId != userId) throw new Exception("User is not the owner of the post");

            if(postToRemove.PostInteractions.Any()) {
                _interactionRepository.RemovePostInteractions(postToRemove.PostInteractions);
            }

            if(postToRemove.Comments.Any()) {
                var comments = postToRemove.Comments.ToList();
                var commentsInteractions = comments.SelectMany(c => c.CommentInteractions).ToList();

                var commentsReplies = comments.SelectMany(c => c.Replies).ToList();

                if(commentsReplies.Any()) {
                    var repliesInteractions = commentsReplies.SelectMany(r => r.CommentInteractions).ToList();
                    _interactionRepository.RemoveCommentInteractions(repliesInteractions);
                    _commentRepository.RemoveComments(commentsReplies);
                }

                _interactionRepository.RemoveCommentInteractions(commentsInteractions);
                _commentRepository.RemoveComments(comments);
            }

            await _postRepository.RemovePostAsync(postToRemove);
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
using System.ComponentModel.DataAnnotations;

namespace api.Infrastructure.Dtos.Post
{
    public class CreatePostDto
    {
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
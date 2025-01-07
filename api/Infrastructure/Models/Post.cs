using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("Posts")]
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostInteractions = new HashSet<PostInteraction>();
        }
        
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        public virtual User User { get; set; } = default!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostInteraction> PostInteractions { get; set; }
    }
}
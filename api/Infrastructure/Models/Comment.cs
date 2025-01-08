using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Comment()
        {
            CommentInteractions = new HashSet<CommentInteraction>();
            Replies = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        [ForeignKey(nameof(ParentComment))]
        public int? ParentCommentId { get; set; }
        public virtual User User { get; set; } = default!;
        public virtual Post Post { get; set; } = default!;
        public virtual Comment? ParentComment { get; set; }
        [InverseProperty(nameof(ParentComment))]
        public virtual ICollection<Comment> Replies { get; set; }
        public virtual ICollection<CommentInteraction> CommentInteractions { get; set; } 
    }
}
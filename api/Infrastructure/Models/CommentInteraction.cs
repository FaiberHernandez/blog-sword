using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("CommentInteractions")]
    public class CommentInteraction
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Value { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(Comment))]
        public int CommentId { get; set; }
        [ForeignKey(nameof(InteractionType))]
        public int InteractionTypeId { get; set; }
        public virtual User User { get; set; } = default!;
        public virtual Comment Comment { get; set; } = default!;
        public virtual InteractionType InteractionType { get; set; } = default!;
    }
}
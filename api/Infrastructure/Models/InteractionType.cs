using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("InteractionTypes")]
    public class InteractionType
    {
        public InteractionType()
        {
            PostInteractions = new HashSet<PostInteraction>();
            CommentInteractions = new HashSet<CommentInteraction>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Code { get; set; } = string.Empty;
        public virtual ICollection<PostInteraction> PostInteractions { get; set; }
        public virtual ICollection<CommentInteraction> CommentInteractions { get; set; }
    }
}
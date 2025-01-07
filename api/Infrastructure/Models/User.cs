using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace api.Infrastructure.Models
{
    [Table("Users")]
    public class User: IdentityUser
    {
        public User()
        {
            UserPermissions = new HashSet<UserPermission>();
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
        }

        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? MiddleName { get; set; }
        [MaxLength(200)]
        public string FirstSurname { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? SecondSurname { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
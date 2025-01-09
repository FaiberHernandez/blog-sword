using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("Permissions")]
    public class Permission
    {
        public Permission()
        {
            UserPermissions = new HashSet<UserPermission>();
            RolePermissions = new HashSet<RolePermissions>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Key { get; set; } = string.Empty;
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
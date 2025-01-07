using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("UserPermissions")]
    public class UserPermission
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(Permission))]
        public int PermissionId { get; set; }
        public virtual User User { get; set; } = default!;
        public virtual Permission Permission { get; set; } = default!;
    }
}
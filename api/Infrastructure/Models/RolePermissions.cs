using System.ComponentModel.DataAnnotations.Schema;

namespace api.Infrastructure.Models
{
    [Table("RolePermissions")]
    public class RolePermissions
    {
        [ForeignKey(nameof(Role))]
        public string RoleId { get; set; } = string.Empty;
        [ForeignKey(nameof(Permission))]
        public int PermissionId { get; set; }
        public virtual Role Role { get; set; } = default!;
        public virtual Permission Permission { get; set; } = default!;
    }
}
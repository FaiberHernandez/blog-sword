using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace api.Infrastructure.Models
{
    [Table("Roles")]
    public class Role: IdentityRole
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }
        
        [MaxLength(200)]
        public string Key { get; set; } = string.Empty;
        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
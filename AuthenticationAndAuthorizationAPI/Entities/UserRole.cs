using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorizationAPI.Entities
{
    public class UserRole
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserId")]
        [Required]
        public int UserId { get; set; }
        [Column("RoleId")]
        [Required]
        public int RoleId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorizationAPI.Entities
{
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Column("Username")]
        [StringLength(50)]
        [Required]
        public string Username { get; set; } = string.Empty;
        [Column("Password")]
        [StringLength(50)]
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
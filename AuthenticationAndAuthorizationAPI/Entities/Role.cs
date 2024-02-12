using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorizationAPI.Entities
{
    public class Role
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Column("Description")]
        [StringLength(100)]
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
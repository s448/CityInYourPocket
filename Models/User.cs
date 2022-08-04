using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInYourPocket.Models
{
    [Table("Users")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(75)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [StringLength(11)]
        public string Phone { get; set; } = string.Empty;

        public DateTime CreationDate { set; get; }
    }
}

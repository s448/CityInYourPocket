using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInYourPocket.Models
{
    public class Charity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(300)]
        public string Describtion { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Website { get; set; } = string.Empty;

        [Required]
        [StringLength(11)]
        public string Phone { get; set; } = string.Empty;

        public string? Image { set; get; }

        [NotMapped]
        public IFormFile? file { set; get; }
    }
}

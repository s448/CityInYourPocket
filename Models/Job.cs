using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInYourPocket.Models
{
    public class Job
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

        [Required]
        [MaxLength(75)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(11)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

    }
}

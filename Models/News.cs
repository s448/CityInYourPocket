using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInYourPocket.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Resource { get; set; } = string.Empty;

        [Required]
        [MinLength(3000)]
        public string Describtion { get; set; } = string.Empty;

        [Required]
        [MaxLength(75)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public DateTime PublishDate { set; get; }

        public string? Image { set; get; }

        [NotMapped]
        public IFormFile? file { set; get; }
    }
}

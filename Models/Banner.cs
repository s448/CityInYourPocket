using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInYourPocket.Models
{
    public class Banner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? BannerImage { get; set; } = string.Empty;
        public string? Phone { get; set;}
        public string? Website { get; set; }

        [NotMapped]
        public IFormFile? file { set; get; }
    }
}

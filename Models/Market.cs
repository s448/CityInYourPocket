using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInYourPocket.Models
{
    public class Market
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }


        //اسم المنتج المراد بيعة
        [Required]
        [MaxLength(75)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(300)]
        public string Describtion { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(11)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public string? Image { set; get; }

        [Required]
        public DateTime PublishDate { set; get; }

        [NotMapped]
        public IFormFile? file { set; get; }
    }
}

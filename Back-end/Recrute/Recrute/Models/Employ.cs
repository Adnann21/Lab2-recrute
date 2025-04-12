using System.ComponentModel.DataAnnotations;
namespace Recrute.Models
{
    public class Employ
    {
        [Key]
        public String Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RecrComp { get; set; }
        [Required]
        public string Region { get; set; }
    }
}
